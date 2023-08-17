import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import AppLayout from "../../components/AppLayout";
import FavoriteContactListView from "./FavoriteContactListView";

const Favorite = () => {
   const navigate = useNavigate();

  useEffect(() => {
    const sessionKey = sessionStorage.getItem("key");

    if (!sessionKey) {
      navigate("/login");
    }
  }, []);
  return (
    <AppLayout>
      <FavoriteContactListView />
    </AppLayout>
  );
};

export default Favorite;
