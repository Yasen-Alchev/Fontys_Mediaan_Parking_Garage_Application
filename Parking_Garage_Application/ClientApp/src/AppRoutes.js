import ControlGate from "./components/ControlGate";
import { Counter } from "./components/Counter";
import { Home } from "./components/Home";
import ParkingSpotsStatus from "./components/ParkingSpotsStatus";
import Payment from "./components/Payment";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/counter',
        element: <Counter />
    },
    {
        path: '/controlGate',
        element: <ControlGate />
    },
    {
        path: '/parkingSpotsStatus',
        element: <ParkingSpotsStatus />
    },
    {
        path: '/payment',
        element: <Payment />
    },

];

export default AppRoutes;
