import React from "react";
import Sidebar from "../../components/Sidebar";
import Navbar from "../../components/Navbar";
import ContactList from "./ContactListView";

const Dashboard = () => {
  return (
    <>
      <div class="flex bg-mainBlack">
        <Sidebar></Sidebar>
        <div class="px-8 text-2xl font-semibold flex-grow">
          <ContactList></ContactList>
        </div>
      </div>
    </>
  );
};

export default Dashboard;
