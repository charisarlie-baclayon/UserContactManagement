import React from "react";
import AppLayout from "../../components/AppLayout";
import ContactListView from "./ContactListView";

const Dashboard = () => {
  return (
    <AppLayout>
      <ContactListView />
    </AppLayout>
  );
};

export default Dashboard;
