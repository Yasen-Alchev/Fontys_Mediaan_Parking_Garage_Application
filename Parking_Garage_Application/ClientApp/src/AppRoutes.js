import ControlGate from "./components/ControlGate";
import { Counter } from "./components/Counter";
import { Home } from "./components/Home";

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
  }
];

export default AppRoutes;
