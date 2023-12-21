import ControlGate from "./components/ControlGate";
import { Counter } from "./components/Counter";
import { Home } from "./components/Home";
import  ParkingSpotsStatus from "./components/ParkingSpotsStatus";
import PaymentPage from "./components/PaymentPage";

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
    <<<<<<< Updated upstream
    path: '/controlGate',
        element: <ControlGate />
    },
    {
        path: '/parkingSpotsStatus',
        element: <ParkingSpotsStatus />
    }
=======
    path: '/controlGate',
    element: <ControlGate />
    },
    {
        path: '/paymentPage',
        element: <PaymentPage />
    }
>>>>>>> Stashed changes
];

export default AppRoutes;
