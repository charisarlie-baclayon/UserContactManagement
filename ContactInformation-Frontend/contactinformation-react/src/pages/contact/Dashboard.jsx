import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import AppLayout from "../../components/AppLayout";
import ContactListView from "./ContactListView";

const Dashboard = () => {
  const navigate = useNavigate();
  const [sessionKey, setSessionKey] = useState(
    sessionStorage.getItem("key") || null
  );

  useEffect(() => {
    if (!sessionKey) {
      navigate("/login");
    }
  }, []);

  return sessionKey ? (
    <AppLayout>
      <ContactListView />
    </AppLayout>
  ) : null;
};

export default Dashboard;
