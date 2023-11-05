import React from 'react';
import { Route, Navigate } from 'react-router-dom';

const PrivateRoute = ({ component: Component, isAuthenticated, ...rest }) => (
    <Route
        {...rest}
        element={
            isAuthenticated ? (
                <Component />
            ) : (
                <Navigate to="/signin" replace />
            )
        }
    />
);

export default PrivateRoute;
