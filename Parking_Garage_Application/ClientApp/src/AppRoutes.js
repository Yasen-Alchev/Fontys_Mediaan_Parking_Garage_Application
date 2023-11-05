import { Counter } from "./components/Counter";
import { Home } from "./components/Home";
import SignIn from "./components/SignIn";

const AppRoutes = [
    {
        index: true,
        element: <Home />,
        protected: false, // Set to false for the homepage
    },
    {
        path: '/counter',
        element: <Counter />,
        protected: true, // Set to true for protected routes
    },
    {
        path: '/signin',
        element: <SignIn />,
        protected: false, // Set to false for the login route
    },
   /* {
        path: '/dashboard',
        element: <Dashboard />,
        protected: true, // Set to true for protected routes
    }*/
];


export default AppRoutes;
