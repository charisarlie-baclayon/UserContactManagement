import React, { useState } from "react";
import { useLocation } from "react-router-dom";

const ContactPopupEditForm = (props) => {
  
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
    try {
      await updateContact(editedContact._id, editedContact);
      // Close the edit form
      props.closePopup();
    } catch (error) {
      console.log(error);
    }
  };

  const handleDelete = async () => {
    try {
      await deleteContact(props.selectedContact._id);
      // Close the edit form
      props.closePopup();
    } catch (error) {
      console.log(error);
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
        <button
          onClick={handleFavoritesToggle}
          className="absolute top-3 right-16 text-whiteText hover:text-darkerPurple"
        >
          {editedContact.favorite
            ? "Remove from Favorites"
            : "Add to Favorites"}
        </button>
        <div className="flex flex-col items-center mb-4 pt-4">{/* ... */}</div>
        <form onSubmit={handleFormSubmit}>
          <div className="flex flex-col gap-y-4">
            <div className="bg-gray-700 rounded-md p-2">
              <label htmlFor="firstName" className="text-sm">
                First Name:
              </label>
              <input
                type="text"
                id="firstName"
                name="firstName"
                value={editedContact.firstName}
                onChange={handleInputChange}
                className="text-lg text-whiterText p-1"
              />
            </div>
            <div className="bg-gray-700 rounded-md p-2">
              <label htmlFor="lastName" className="text-sm">
                Last Name:
              </label>
              <input
                type="text"
                id="lastName"
                name="lastName"
                value={editedContact.lastName}
                onChange={handleInputChange}
                className="text-lg text-whiterText p-1"
              />
            </div>
            {/* ... Add more form fields here ... */}
          </div>
          <div className="flex gap-x-2 mt-4">
            <button
              type="submit"
              className="bg-green-500 text-white px-4 py-2 rounded-lg"
            >
              Save
            </button>
            <button
              type="button"
              onClick={handleDelete}
              className="bg-red-500 text-white px-4 py-2 rounded-lg"
            >
              Delete
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default ContactPopupEditForm;
