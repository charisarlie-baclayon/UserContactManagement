import React, { useState } from "react";
import { BiPencil } from "react-icons/bi";
import AddressForm from "./AddressForm";

const ContactAddress = (props) => {
  const [isEditFormOpen, setIsEditFormOpen] = useState(false);
  const [selectedAddress, setSelectedAddress] = useState(null);

  const handleEditClick = (address) => {
    setSelectedAddress(address);
    setIsEditFormOpen(true);
  };
  return (
    <div className="relative flex flex-col bg-gray-700 rounded-md p-4">
      <div className="flex justify-between mb-2">
        <span className="text-base">Addresses:</span>
        <button
          onClick={props.onEditClick} // Use the passed handler here
          className="text-whiteText hover:text-darkerPurple"
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
      </div>

      <div>
        <ul>
          {props.addresses.map((address, index) => (
            <li
              key={index}
              className="mb-2 cursor-pointer"
              onClick={() => handleEditClick(address)} // Handle click to edit address
            >
              <p className="text-sm text-whiteText">{address.addressType}</p>
              <p className="text-lg">{address.addressDescription}</p>
            </li>
          ))}
        </ul>
      </div>
      {isEditFormOpen && (
        <AddressForm
          selectedAddress={selectedAddress}
          contactId={props.contactId}
          closePopup={() => setIsEditFormOpen(false)}
        />
      )}
    </div>
  );
};

export default ContactAddress;
