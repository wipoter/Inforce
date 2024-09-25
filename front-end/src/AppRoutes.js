import React from 'react';
import {Home} from './Home/Home';
import {Login} from "./Login/Login";
import Info from "./Info/Info";

const AppRoutes = [
    {
        path: '/',
        element: <Home />
    },
    {
        path: '/login',
        element: <Login />
    },
    {
        path: '/info/:id',
        element: <Info />,
        private: true
    }

];

export default AppRoutes;

