import React from 'react';
import { Navigate, Route } from 'react-router-dom';
import { UserContext } from '../contexts/UserContext';

export function PrivateRoute({ path, element }) {
    const { user } = UserContext();

    if (!user) {
        return <Navigate to="/" />;
    }

    return <Route path={path} element={element} />;
}
