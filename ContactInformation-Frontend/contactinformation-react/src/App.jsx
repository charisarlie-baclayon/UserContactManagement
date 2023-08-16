import { Routes, Route } from "react-router-dom";
import Login from "./pages/userauth/Login";

const App = () => {
  return (
    <>
      <Routes>
        <Route path="/login" element={<Login></Login>}></Route>
      </Routes>
    </>
  );
};

export default App;
