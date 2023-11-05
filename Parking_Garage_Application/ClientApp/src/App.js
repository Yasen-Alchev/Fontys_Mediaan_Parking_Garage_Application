import React, { Component } from 'react';
import { Routes, Route, useNavigate } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import PrivateRoute from './components/PrivateRoute';
import SignIn from './components/SignIn';

export default class App extends Component {
    static displayName = App.name;

    // Initialize the authentication status
    state = {
        isAuthenticated: false,
    };

    // A function to update the authentication status
    updateAuthStatus = (isAuthenticated) => {
        this.setState({ isAuthenticated });
    };

    render() {
        return (
            <Layout>
                <Routes>
                    {AppRoutes.map((route, index) => {
                        const { element, ...rest } = route;
                        return (
                            <Route
                                key={index}
                                {...rest}
                                element={
                                    route.path === '/signin' ? (
                                        <SignIn updateAuthStatus={this.updateAuthStatus} />
                                    ) : route.protected ? (
                                        <PrivateRoute
                                            element={element}
                                            isAuthenticated={this.state.isAuthenticated}
                                        />
                                    ) : (
                                        element
                                    )
                                }
                            />
                        );
                    })}
                    {/* Add a catch-all route for non-existent routes */}
                    <Route path="*" element={<div>Not Found</div>} />
                </Routes>
            </Layout>
        );
    }
}
