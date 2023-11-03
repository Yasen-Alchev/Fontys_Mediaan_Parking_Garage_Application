import { useEffect, useState, useContext } from 'react';
import { jwtDecode } from "jwt-decode";
import { UserContext } from '../contexts/UserContext';

/*import { UserService } from "../../../../Service/Contracts"
import { CreateUserDTO } from "../../../../DataModels/DTO"*/


export default function SignIn() {
    const { user, setUser } = useContext(UserContext);

    useEffect(() => {
        console.log('User data has changed: ', user);
    }, [user]);

    function handleCallbackResponse(response) {
        var userObject = jwtDecode(response.credential);
        setUser(userObject);
 
        document.getElementById("signInDiv").hidden = true;

/*        const userDTO = new CreateUserDTO("name", "email@gmail.com", "strongpassword123", 33, 1);
        UserService.CreateUser(userDTO);*/
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
