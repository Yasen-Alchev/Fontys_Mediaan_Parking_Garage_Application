import ControlGate from "./components/ControlGate";
import { Counter } from "./components/Counter";
import { Home } from "./components/Home";
import  ParkingSpotsStatus from "./components/ParkingSpotsStatus";
import PaymentPage from "./components/PaymentPage";
import CarRegistration from "./components/CarRegistration";

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
        path: '/paymentPage',
            element: <PaymentPage />
    },
    {
        path: '/carRegistration',
        element: <CarRegistration />
    }
];

export default AppRoutes;
