import { Routes, Route } from "react-router-dom";
import LoginView from "./pages/userauth/LoginView";
import RegisterView from "./pages/userauth/RegisterView";

const App = () => {
  return (
    <>
      <Routes>
        <Route path="/login" element={<LoginView></LoginView>}></Route>
        <Route path="/register" element={<RegisterView></RegisterView>}></Route>
      </Routes>
    </>
  );
};

export default App;
