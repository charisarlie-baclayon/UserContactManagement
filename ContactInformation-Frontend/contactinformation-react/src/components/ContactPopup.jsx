import React from "react";

const ContactPopup = (props) => {
  return (
    <div className="fixed inset-0 flex justify-center items-center bg-black bg-opacity-50">
      <div className="bg-gray-800 p-5 rounded-lg text-white relative w-96">
        <button
          onClick={props.closePopup}
          className="absolute top-3 right-3 text-gray-300"
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
          </svg>
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
            <p className=" text-lg text-whiterText">
              {props.selectedContact.emailAddress}
            </p>
          </div>
          <div className=" bg-gray-700 rounded-md p-2">
            <span className="text-sm">Phone Number:</span>{" "}
            <p className="text-lg">{props.selectedContact.phoneNumber}</p>
          </div>
          <div className=" bg-gray-700 rounded-md p-2">
            <span className="text-sm">Birthday:</span>{" "}
            <p className=" text-lg">
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
          <div className=" bg-gray-700 rounded-md p-2">
            <span className="text-base">Addresses:</span>{" "}
            <ul className="">
              {props.selectedContact.addresses.map((address, index) => (
                <li key={index} className="mb-2">
                  <p className="text-sm text-whiteText">
                    {address.addressType}
                  </p>
                  <p className="text-lg">{address.addressDescription}</p>
                </li>
              ))}
            </ul>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ContactPopup;
