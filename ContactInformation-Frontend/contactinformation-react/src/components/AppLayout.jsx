import React from "react";
import Sidebar from "./Sidebar";

const AppLayout = ({ children }) => {
  return (
    <div className="flex bg-mainBlack">
      <Sidebar />
      <div className="px-8 text-2xl font-semibold flex-grow">{children}</div>
    </div>
  );
};

export default AppLayout;
