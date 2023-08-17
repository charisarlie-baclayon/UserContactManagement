import React from "react";

const ContactAddress = (props) => {
  return (
    <div className=" bg-gray-700 rounded-md p-2">
      <span className="text-base">Addresses:</span>
      <ul>
        {props.addresses.map((address, index) => (
          <li key={index} className="mb-2">
            <p className="text-sm text-whiteText">{address.addressType}</p>
            <p className="text-lg">{address.addressDescription}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ContactAddress;
