import logo from './logo.svg';
import './App.css';
import ListaDeMaterias from './Pages/ListaDeMaterias/ListaDeMaterias';
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { ToastContainer } from 'react-toastify';
import Login from './Pages/Login/Login';

function App() {
  const router = createBrowserRouter([
    {
      path: "/login",
      element: <Login />,
    },
    {
      path: "/",
      element: <ListaDeMaterias />,
    },
  ]);
  return (
    <>
      <ToastContainer />
      <RouterProvider router={router} />
    </>
  );
}

export default App;
