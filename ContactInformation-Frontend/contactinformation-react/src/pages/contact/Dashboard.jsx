import React from "react";
import Sidebar from "../../components/Sidebar";
import ContactList from "./ContactListView";

const Dashboard = () => {
  return (
    <>
      <div class="flex bg-mainBlack">
        <Sidebar></Sidebar>
        <div class="p-8 text-2xl font-semibold flex-1 h-screen">
          <ContactList></ContactList>
        </div>
      </div>
    </>
  );
};

export default Dashboard;
