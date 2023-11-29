import { useEffect, _ , useContext } from 'react';
import { jwtDecode } from "jwt-decode";
import { UserContext } from '../contexts/UserContext';
import Cookies from 'universal-cookie';

export default function SignIn() {
    const { user, setUser } = useContext(UserContext);

    function handleCallbackResponse(response) {
        var userObject = jwtDecode(response.credential);
        document.getElementById("signInDiv").hidden = true;

        const cookies = new Cookies();
        cookies.set('GoogleJWTToken', userObject, { path: '/', secure: true });

        const userDTO = {
            name: userObject.name,
            email: userObject.email,
            picture: userObject.picture,
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
                    // Parse the response to get the user object
                    return response.json();
                } else {
                    console.error('Failed to send user data to the backend.');
                }
            })
            .then(data => {
                setUser({
                    ...userObject,
                    ...data
                });
            })
            .catch(error => {
                console.error('An error occurred while sending user data.');
            });
    }


    function handleSignOut(event) {
        setUser({});
        const cookies = new Cookies();
        cookies.remove('GoogleJWTToken');
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

        console.log("singin.js user: ");
        console.log(user);
        if (Object.keys(user).length > 0) {
            document.getElementById("signInDiv").hidden = true;
        }

    }, [user])

    return (
        <div className="App">
            <div id="signInDiv"></div>
            {Object.keys(user).length !== 0 &&
                <button onClick={(e) => handleSignOut(e)}>Sign Out</button>
            }

            {user &&
                <div>
                    <img src={user.picture} alt="User"></img>
                    <h3>{user.name}</h3>
                </div>
            }
        </div>
    );
}
