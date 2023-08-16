import { Routes, Route } from "react-router-dom";
import LoginView from "./pages/userauth/LoginView";
import RegisterView from "./pages/userauth/RegisterView";
import Dashboard from "./pages/contact/Dashboard";

const App = () => {
  return (
    <>
      <Routes>
        <Route path="/login" element={<LoginView></LoginView>}></Route>
        <Route path="/register" element={<RegisterView></RegisterView>}></Route>
        <Route path="/dashboard" element={<Dashboard></Dashboard>}></Route>
      </Routes>
    </>
  );
};

export default App;
