import React, { useState } from "react";
import ContactPopupEditForm from "./ContactPopupEditForm";
import ContactAddress from "./ContactAddress"; 
import AddressForm from "./AddressForm";

const ContactPopup = (props) => {
  const [isEditFormOpen, setIsEditFormOpen] = useState(false);
  const [isAddressFormOpen, setIsAddressFormOpen] = useState(false);

  const handleEditClick = () => {
    setIsEditFormOpen(true);
  };

  const handleAddressEditClick = () => {
    setIsAddressFormOpen(true);
  };

  return (
    <div className="fixed inset-0 flex justify-center items-center bg-black bg-opacity-50">
      <div className="relative bg-gray-800 p-5 rounded-lg text-white w-full max-w-md mx-auto md:w-96">
        <button
          onClick={props.closePopup}
          className="absolute top-3 left-3 text-whiteText hover:text-darkerPurple"
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
              d="M6 18L18 6M6 6l12 12"
            />
          </svg>{" "}
        </button>
        <button
          onClick={handleEditClick}
          className="absolute top-3 right-3 text-whiteText hover:text-darkerPurple"
        >
          Edit
        </button>
        <div className="flex flex-col items-center mb-4 pt-4">
          <img
            className="h-20 w-20 rounded-full mb-2"
            src="../src/assets/people.png" // Replace with the actual image source
            alt={`${props.selectedContact.firstName} ${props.selectedContact.lastName}`}
          />
          <h2 className="text-md font-semibold text-center mb-2 ">
            {props.selectedContact.firstName} {props.selectedContact.lastName}
          </h2>
        </div>
        <div className="flex flex-col gap-y-4">
          <div className=" bg-gray-700 rounded-md p-2">
            <span className="text-sm">Email:</span>{" "}
            <p className="text-lg text-whiterText">
              {props.selectedContact.emailAddress}
            </p>
          </div>
          <div className=" bg-gray-700 rounded-md p-2">
            <span className="text-sm">Mobile Number:</span>{" "}
            <p className="text-lg">{props.selectedContact.phoneNumber}</p>
          </div>
          <div className=" bg-gray-700 rounded-md p-2">
            <span className="text-sm">Birthday:</span>{" "}
            <p className="text-lg">
              {new Date(props.selectedContact.birthDate).toLocaleDateString(
                undefined,
                {
                  year: "numeric",
                  month: "long",
                  day: "numeric",
                }
              )}
            </p>
          </div>
          <ContactAddress
            addresses={props.selectedContact.addresses}
            onEditClick={handleAddressEditClick}
            contactId={props.selectedContact.id} // Pass the handler here
          />
        </div>
      </div>
      {isEditFormOpen && (
        <ContactPopupEditForm
          selectedContact={props.selectedContact}
          closePopup={() => setIsEditFormOpen(false)}
        />
      )}
      {isAddressFormOpen && (
        <AddressForm
          contactId={props.selectedContact.id} // Pass the contact ID
          closePopup={() => setIsAddressFormOpen(false)} // Pass the close handler
        />
      )}
    </div>
  );
};

export default ContactPopup;
