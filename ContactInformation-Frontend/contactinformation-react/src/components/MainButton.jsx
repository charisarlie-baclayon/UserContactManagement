import React from "react";

const CustomButton = (props) => {
  return (
    <button
      className="bg-darkerPurple rounded text-darkBlack py-2 font-bold transition duration-300 ease-in-out hover:underline hover:scale-105"
      onClick={props.onClick}
    >
      {props.text}
    </button>
  );
};

export default CustomButton;
