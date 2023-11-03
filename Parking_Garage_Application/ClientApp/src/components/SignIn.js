import { useEffect, useState, useContext } from 'react';
import { jwtDecode } from "jwt-decode";
import { UserContext } from '../contexts/UserContext';

export default function SignIn() {
    const { user, setUser } = useContext(UserContext);

    function handleCallbackResponse(response) {
        var userObject = jwtDecode(response.credential);
        document.getElementById("signInDiv").hidden = true;

        setUser(userObject);

        const userDTO = {
            name: userObject.name,
            email: userObject.email,
            role: 0
        };

        fetch('api/user', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userDTO),
        })
            .then(response => {
                if (response.ok) {
                    console.log('User data has been sent to the backend.');
                } else {
                    console.error('Failed to send user data to the backend.');
                }
            })
            .catch(error => {
                console.error('An error occurred while sending user data.');
            });

    }

    function handleSignOut(event) {
        setUser({});
        document.getElementById("signInDiv").hidden = false;
    }

    useEffect(() => {
        /* global google */
        google.accounts.id.initialize({
            client_id: "1080486814516-a9nulf3utvae566inrjisimbf9p0ecvl.apps.googleusercontent.com",
            callback: handleCallbackResponse
        })

        google.accounts.id.renderButton(
            document.getElementById("signInDiv"),
            { theme: "outline", size: "large" }
        )
    }, [])

    return (
        <div className="App">
            <div id="signInDiv"></div>
            {Object.keys(user).length != 0 &&
                <button onClick={(e) => handleSignOut(e)}>Sign Out</button>
            }

            {user &&
                <div>
                    <img src={user.picture}></img>
                    <h3>{user.name}</h3>
                </div>
            }
        </div>
    );
}
