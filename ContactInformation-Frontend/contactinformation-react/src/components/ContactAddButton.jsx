import React from "react";

const ContactAddButton = (props) => {
  return (
    <button
      className="fixed m-4 mr-8 bottom-4 right-4 p-2 bg-accentPurple text-white rounded-full shadow-lg transition duration-300 ease-in-out hover:scale-125"
      onClick={props.onClick}
    >
      <svg
        xmlns="http://www.w3.org/2000/svg"
        className="h-8 w-8"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth={2}
          d="M12 6v12M6 12h12"
        />
      </svg>
    </button>
  );
};

export default ContactAddButton;
