import React, { useLayoutEffect, useContext, useState } from 'react';
import './Styles/styles.css';
import { Route, Routes, Navigate } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import Cookies from 'universal-cookie';
import { UserContext, UserProvider } from './contexts/UserContext';
import { Home } from "./components/Home";

function LoadingIndicator() {
    return (
        <div className="loading-indicator">
            Loading...
        </div>
    );
}

function App() {
    const { user, setUser } = useContext(UserContext);
    const [initialized, setInitialized] = useState(false);

    useLayoutEffect(() => {
        const cookies = new Cookies();
        const token = cookies.get('GoogleJWTToken');

        const fetchUserData = async () => {
            try {
                const response = await fetch('api/user', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        name: token.name,
                        email: token.email,
                        picture: token.picture,
                        role: 0
                    }),
                });

                if (response.ok) {
                    const userData = await response.json();

                    // Merge data from token and server response
                    const mergedUserData = {
                        ...token,  // data from the token
                        ...userData  // data from the server response
                    };

                    setUser(mergedUserData);
                } else {
                    console.error('Failed to fetch user data.');
                }
            } catch (error) {
                console.error('An error occurred while fetching user data.', error);
            } finally {
                setInitialized(true);
            }
        };

        if (token) {
            // If the user is logged in, fetch additional data
            fetchUserData();
        } else {
            setInitialized(true);
        }
    }, [setUser, setInitialized]);


    if (!initialized) {
        return <LoadingIndicator />;
    }

    return (
        <UserProvider>
        <Layout>
            <Routes>
                <Route path="/" element={<Home />} />
                {Object.keys(user).length > 0 ? (
                    // Render all routes if the user is logged in
                    AppRoutes.map((route, index) => (
                        <Route key={index} {...route} />
                    ))
                ) : (
                    // Redirect to home if the user is not logged in
                    <Route path="*" element={<Navigate to="/" />} />
                )}
            </Routes>

            </Layout>
        </UserProvider>
    );
}

export default App;
