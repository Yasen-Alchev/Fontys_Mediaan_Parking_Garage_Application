import ControlGate from "./components/ControlGate";
import { Counter } from "./components/Counter";
import { Home } from "./components/Home";
import  ParkingSpotsStatus from "./components/ParkingSpotsStatus";

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
    }
];

export default AppRoutes;
