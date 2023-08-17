import React from "react";

const ContactHeader = (props) => {

  return (
    <div className="sticky top-0 w-full p-4 bg-mainBlack flex justify-between items-center">
      <h1 className="text-2xl text-whiterText">{props.title}</h1>
      <input
        type="text"
        className="text-sm w-96 p-2.5 pl-4 rounded-full bg-gray-800 text-whiteText placeholder-gray-500 focus:outline-none"
        placeholder="Search contacts..."
        value={props.searchTerm}
        onChange={(e) => props.setSearchTerm(e.target.value)}
      />
    </div>
  );
};

export default ContactHeader;
