import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
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
    path: '/fetch-data',
    element: <FetchData />
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
