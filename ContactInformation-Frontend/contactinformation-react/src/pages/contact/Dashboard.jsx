import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import AppLayout from "../../components/AppLayout";
import ContactListView from "./ContactListView";

const Dashboard = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const sessionKey = sessionStorage.getItem("key");

    if (!sessionKey) {
      navigate("/login");
    }
  }, []);

  return (
    <AppLayout>
      <ContactListView />
    </AppLayout>
  );
};

export default Dashboard;
