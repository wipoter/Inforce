import React from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import './App.css';
import AppRoutes from "./AppRoutes";
import { Route, Routes } from "react-router-dom";
import { Layout } from "./Services/Layout";
import PrivateRoute from "./Services/PrivateRoute";

function App() {
    return (
        <Router>
            <Layout>
                <Routes>
                    {AppRoutes.map((route, index) => {
                        if (route.private) {
                            return (
                                <Route
                                    key={index}
                                    path={route.path}
                                    element={
                                        <PrivateRoute>
                                            {route.element}
                                        </PrivateRoute>
                                    }
                                />
                            );
                        }

                        return (
                            <Route
                                key={index}
                                path={route.path}
                                element={route.element}
                            />
                        );
                    })}
                </Routes>
            </Layout>
        </Router>
    );
}

export default App;