import { Home } from "./components/Home";
import { TranslatorManagement } from "./components/TranslatorManagement";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/translator',
    element: <TranslatorManagement />
  }
];

export default AppRoutes;
