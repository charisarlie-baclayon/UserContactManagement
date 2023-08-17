import { Routes, Route } from "react-router-dom";
import LoginView from "./pages/userauth/LoginView";
import RegisterView from "./pages/userauth/RegisterView";
import Dashboard from "./pages/contact/Dashboard";
import FavoriteContactListView from "./pages/contact/FavoriteContactListView";
import UserSettings from "./pages/user/UserSettingsView";

const App = () => {
  return (
    <>
      <Routes>
        <Route path="/login" element={<LoginView></LoginView>}></Route>
        <Route path="/register" element={<RegisterView></RegisterView>}></Route>
        <Route path="/" element={<Dashboard></Dashboard>}></Route>
        <Route
          path="/favorites"
          element={<FavoriteContactListView></FavoriteContactListView>}
        ></Route>
        <Route path="/settings" element={<UserSettings></UserSettings>}></Route>
      </Routes>
    </>
  );
};

export default App;
