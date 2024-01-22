import { useEffect, useState, useContext } from "react";
import { jwtDecode } from "jwt-decode";
import { UserContext } from "../contexts/UserContext";
import Cookies from "universal-cookie";

export default function SignIn() {
    const { user, setUser } = useContext(UserContext);

    function handleCallbackResponse(response) {
        var userObject = jwtDecode(response.credential);
        document.getElementById("signInDiv").hidden = true;

        const cookies = new Cookies();
        cookies.set("GoogleJWTToken", userObject, { path: "/", secure: true });

        const userDTO = {
            name: userObject.name,
            email: userObject.email,
            picture: userObject.picture,
            role: 0,
        };

        fetch("api/user", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(userDTO),
        })
            .then((response) => {
                if (response.ok) {
                    console.log("User data has been sent to the backend.");
                    // Parse the response to get the user object
                    return response.json();
                } else {
                    console.error("Failed to send user data to the backend.");
                }
            })
            .then((data) => {
                setUser({
                    ...userObject,
                    ...data,
                });
            })
            .catch((error) => {
                console.error("An error occurred while sending user data.");
            });
    }

    function handleSignOut(event) {
        setUser({});
        const cookies = new Cookies();
        cookies.remove("GoogleJWTToken");
        document.getElementById("signInDiv").hidden = false;
    }

    useEffect(() => {
        /* global google */
        google.accounts.id.initialize({
            client_id:
                "1080486814516-a9nulf3utvae566inrjisimbf9p0ecvl.apps.googleusercontent.com",
            callback: handleCallbackResponse,
        });

        google.accounts.id.renderButton(document.getElementById("signInDiv"), {
            theme: "outline",
            size: "large",
        });

        console.log("singin.js user: ");
        console.log(user);
        if (Object.keys(user).length > 0) {
            document.getElementById("signInDiv").hidden = true;
        }
    }, [user]);

    return (
        <div className="App">
            <div id="signInDiv"></div>
            {Object.keys(user).length !== 0 && (
                <div className="flex flex-row border p-1 rounded bg-slate-50">
                    <div>
                        <h5>{user.name}</h5>
                        <button className="px-2 py-1 text-sm bg-blue-500 text-white rounded hover:bg-blue-700" onClick={(e) => handleSignOut(e)}>Sign Out</button>
                    </div>
                    <img src={user.picture} alt="User" className="rounded-full max-h-16 m-1"></img>
                </div>
            )}
        </div>
    );
}
