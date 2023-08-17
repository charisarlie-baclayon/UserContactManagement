import React, { useState } from "react";
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { deleteContact, updateContact } from "../api/contact/apiContact";

const ContactPopupEditForm = (props) => {
  
  const navigate = useNavigate();
  const [editedContact, setEditedContact] = useState({
    ...props.selectedContact,
  });

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setEditedContact((prevContact) => ({
      ...prevContact,
      [name]: value,
    }));
  };

  const handleFavoritesToggle = () => {
    setEditedContact((prevContact) => ({
      ...prevContact,
      favorite: !prevContact.favorite,
    }));
  };

  const handleFormSubmit = async (event) => {
    event.preventDefault();
    const confirmed = window.confirm(
      "Are you sure you want to update this contact?"
    );
    if (confirmed) {
      try {
        await updateContact(editedContact.id, editedContact);
        // Close the edit form
        props.closePopup();
        window.location.reload();// Navigate to "/"
      } catch (error) {
        console.log(error);
      }
    }
  };

  const handleDelete = async () => {
    const confirmed = window.confirm(
      "Are you sure you want to delete this contact?"
    );
    if (confirmed) {
      try {
        await deleteContact(props.selectedContact.id);
        // Close the edit form
        props.closePopup();
        window.location.reload(); // Navigate to "/"
      } catch (error) {
        console.log(error);
      }
    }
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
        <div className="flex flex-col gap-y-2 pt-8">
          <form onSubmit={handleFormSubmit}>
            <div className="flex flex-col gap-y-2">
              <div>
                <span className="text-sm">First Name:</span>
                <div className="rounded-md">
                  {""}
                  <input
                    type="text"
                    name="firstName"
                    value={editedContact.firstName}
                    onChange={handleInputChange}
                    className="text-lg text-whiterText p-1 bg-gray-700 rounded-md w-full"
                  />
                </div>
              </div>
              <div>
                <span className="text-sm">Last Name:</span>
                <div className="rounded-md">
                  {""}
                  <input
                    type="text"
                    name="lastName"
                    value={editedContact.lastName}
                    onChange={handleInputChange}
                    className="text-lg text-whiterText p-1 bg-gray-700 rounded-md w-full"
                  />
                </div>
              </div>
              <div>
                <span className="text-sm">PhoneNumber:</span>
                <div className="rounded-md">
                  {""}
                  <input
                    type="text"
                    name="phoneNumber"
                    value={editedContact.phoneNumber}
                    onChange={handleInputChange}
                    className="text-lg text-whiterText p-1 bg-gray-700 rounded-md w-full"
                  />
                </div>
              </div>
              <div>
                <span className="text-sm">Email:</span>
                <div className="rounded-md">
                  {""}
                  <input
                    type="text"
                    name="emailAddress"
                    value={editedContact.emailAddress}
                    onChange={handleInputChange}
                    className="text-lg text-whiterText p-1 bg-gray-700 rounded-md w-full"
                  />
                </div>
              </div>
              <div>
                <span className="text-sm">Birthday:</span>
                <div className="rounded-md">
                  {""}
                  <input
                    type="date"
                    name="birthDate"
                    value={editedContact.birthDate}
                    onChange={handleInputChange}
                    className="text-lg text-whiterText p-1 bg-gray-700 rounded-md w-full"
                  />
                </div>
              </div>
            </div>
            <div className="flex flex-col gap-y-4 mt-8">
              <button
                onClick={handleFavoritesToggle}
                className=" right-16 text-whiteText hover:text-darkerPurple text-lg mb-2"
              >
                {editedContact.favorite
                  ? "Remove from Favorites"
                  : "Add to Favorites"}
              </button>
              <button
                type="submit"
                className="bg-darkerPurple text-white px-4 py-2 rounded-lg transition duration-300 ease-in-out hover:scale-105"
              >
                Save
              </button>
              <button
                type="button"
                onClick={handleDelete}
                className="bg-transparent border border-red-400 text-red-400 px-4 py-2 rounded-lg transition duration-300 ease-in-out hover:underline hover:scale-105"
              >
                Delete
              </button>
            </div>
          </form>
        </div>
        <div className="flex flex-col items-center mb-4 pt-4"></div>
      </div>
    </div>
  );
};

export default ContactPopupEditForm;
