import React, { useEffect, useState } from 'react';
import { jwtDecode } from "jwt-decode";

export default function SignIn() {
    const [user, setUser] = useState({});
    const [isSignedIn, setIsSignedIn] = useState(false);

    function handleCallbackResponse(response) {
        var userObject = jwtDecode(response.credential);
        setUser(userObject);
        setIsSignedIn(true);
    }

    function handleSignOut() {
        setUser({});
        setIsSignedIn(false);
        // Add sign-out logic if necessary
    }

    useEffect(() => {
        /* global google */
        google.accounts.id.initialize({
            client_id: "1080486814516-a9nulf3utvae566inrjisimbf9p0ecvl.apps.googleusercontent.com",
            callback: handleCallbackResponse,
            onerror: (error) => {
                console.error("Google Sign-In initialization error:", error);
            }
        });

        google.accounts.id.renderButton(
            document.getElementById("signInDiv"),
            { theme: "outline", size: "large" }
        );
    }, []);

    return (
        <div className="App">
            <div id="signInDiv"></div>
            {isSignedIn ? (
                <div>
                    <button onClick={handleSignOut}>Sign Out</button>
                    <div>
                        <img src={user.picture} alt="User profile" />
                        <h3>{user.name}</h3>
                    </div>
                </div>
            ) : null}
        </div>
    );
}
