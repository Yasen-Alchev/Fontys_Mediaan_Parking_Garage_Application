import React, { useLayoutEffect, useContext, useState } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import Cookies from 'universal-cookie';
import { UserContext, UserProvider } from './contexts/UserContext';
import { Home } from "./components/Home";
import { Counter } from "./components/Counter";
import { PrivateRoute } from "./components/PrivateRoute";

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

        if (token) {
            const userDTO = {
                name: token.name,
                email: token.email,
                picture: token.picture,
                role: 0
            };
            setUser(userDTO);
            setInitialized(true);
      
        } else {
            setInitialized(true);
        }
    }, [setUser]);

    if (!initialized) {
        return <LoadingIndicator />;
    }

    return (
        <UserProvider>
            <Layout>
                <Routes>
                    <Route path="/" element={<Home />} />
                    {Object.keys(user).length > 0 ? (
                        // Render all router is the user is logged in
                        AppRoutes.map((route, index) => (
                            <Route key={index} {...route} />
                        ))
                    ) : (
                        // Redirect to home is user is not logged in
                        <Route path="*" element={<Navigate to="/" />} />
                    )}
                </Routes>

            </Layout>
        </UserProvider>
    );
}

export default App;
