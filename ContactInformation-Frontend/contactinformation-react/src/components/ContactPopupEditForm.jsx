import React, { useState } from "react";
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import {
  deleteContact,
  updateContact,
  createContact,
} from "../api/contact/apiContact";

const ContactPopupEditForm = (props) => {
  const navigate = useNavigate();
  const [editedContact, setEditedContact] = useState(
    props.selectedContact || {
      firstName: "",
      lastName: "",
      phoneNumber: "",
      emailAddress: "",
      birthDate: "",
      favorite: false,
    }
  );

  const [validationErrors, setValidationErrors] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: "",
    emailAddress: "",
    birthDate: "",
    favorite: "",
    success: "",
  });

  const isCreateMode = props.isCreateMode;

  const validate = () => {
    const errors = {};

    if (!editedContact.firstName) {
      errors.firstName = "First name is required.";
    } else if (
      editedContact.firstName.length < 2 ||
      editedContact.firstName.length > 50
    ) {
      errors.firstName = "First name must be between 2 and 50 characters.";
    }

    if (!editedContact.lastName) {
      errors.lastName = "Last name is required.";
    } else if (
      editedContact.lastName.length < 2 ||
      editedContact.lastName.length > 50
    ) {
      errors.lastName = "Last name must be between 2 and 50 characters.";
    }

    // Validate phoneNumber
    if (!editedContact.phoneNumber) {
      errors.phoneNumber = "Phone number is required.";
    } else if (!/^\d{11}$/.test(editedContact.phoneNumber)) {
      errors.phoneNumber = "Phone number must be an 11-digit number.";
    }

    // Validate emailAddress
    if (!editedContact.emailAddress) {
      errors.emailAddress = "Email address is required.";
    } else if (
      !/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(
        editedContact.emailAddress
      )
    ) {
      errors.emailAddress = "Invalid email address.";
    }

    // Validate birthDate
    if (!editedContact.birthDate) {
      errors.birthDate = "Birth date is required.";
    }

    return Object.keys(errors).length === 0 ? null : errors;
  };

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

  const handleFormSubmit = async (e) => {
    e.preventDefault();
    try {
      const errors = validate();
      if (errors) {
        setValidationErrors(errors);
      } else {
        setValidationErrors({});
        const confirmed = window.confirm(
          `Are you sure you want to ${
            isCreateMode ? "create" : "update"
          } this contact?`
        );
        if (confirmed) {
          try {
            if (isCreateMode) {
              try {
                const response = await createContact(editedContact);
                if (response.status === 200) {
                  // Close the edit form
                  props.closePopup();
                  window.location.reload();
                }
              } catch (error) {
                if (error.response) {
                  if (error.response.status === 500) {
                    console.error("Server error:", error.response.data);
                    setValidationErrors({
                      success: "Something went wrong.",
                    });
                  } else {
                    console.error(
                      "Unknown error occured during creation:",
                      error
                    );
                    setValidationErrors({
                      success: "Something went wrong.",
                    });
                  }
                }
              }
            } else {
              try {
                const response = await updateContact(
                  editedContact.id,
                  editedContact
                );
                if (response.status === 200) {
                  // Close the edit form
                  props.closePopup();
                  window.location.reload();
                }
              } catch (error) {
                if (error.response) {
                  if (error.response === 404) {
                    console.error("Contact not found:", error);
                    console.error("Error during registration:", error);
                    setValidationErrors({
                      success: "Contact not found.",
                    });
                  } else if (response.status === 500) {
                    console.error("Server error:", error.response.data);
                  } else {
                    console.error(
                      "Unknown error occured during updation:",
                      error
                    );
                    // setValidationErrors({
                    //   success: "Something went wrong.",
                    // });
                  }
                }
              }
            }
          } catch (error) {
            console.log("An unknow error occurred:", error);
            // setValidationErrors({
            //   success: "Something went wrong.",
            // });
          }
        }
      }
    } catch (error) {
      console.log("An error occurred:", error);
      // setValidationErrors({
      //   success: "Something went wrong.",
      // });
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
        window.location.reload();
      } catch (error) {
        if (error.response) {
          if (error.response.status === 404) {
            console.error("Contact not found");
            setValidationErrors({
              success: "Contact does not exist.",
            });
          } else if (error.response.status === 500) {
            console.error("Server error");
            setValidationErrors({
              success: "Something went wrong.",
            });
          } else {
            console.error("Unknown error occurred");
            setValidationErrors({
              success: "Something went wrong.",
            });
          }
        } else {
          console.error("An error occurred:", error);
          setValidationErrors({
            success: "Something went wrong.",
          });
        }
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
                  <div className="text-red-500 text-sm">
                    {validationErrors.firstName}
                  </div>
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
                  <div className="text-red-500 text-sm">
                    {validationErrors.lastName}
                  </div>
                </div>
              </div>
              <div>
                <span className="text-sm">Mobile Number:</span>
                <div className="rounded-md">
                  {""}
                  <input
                    type="text"
                    name="phoneNumber"
                    value={editedContact.phoneNumber}
                    onChange={handleInputChange}
                    className="text-lg text-whiterText p-1 bg-gray-700 rounded-md w-full"
                  />
                  <div className="text-red-500 text-sm">
                    {validationErrors.phoneNumber}
                  </div>
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
                  <div className="text-red-500 text-sm">
                    {validationErrors.emailAddress}
                  </div>
                </div>
              </div>
              <div>
                <span className="text-sm">Birthday:</span>
                <div className="rounded-md">
                  {""}
                  <input
                    type="date"
                    pattern="\d{4}-\d{2}-\d{2}"
                    name="birthDate"
                    value={editedContact.birthDate}
                    onChange={handleInputChange}
                    className="text-lg text-whiterText p-1 bg-gray-700 rounded-md w-full"
                  />
                  <div className="text-red-500 text-sm">
                    {validationErrors.birthDate}
                  </div>
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
              <div className="text-red-500 text-sm text-center">
                {validationErrors.success}
              </div>
              <button
                type="submit"
                className="bg-darkerPurple text-white px-4 py-2 rounded-lg transition duration-300 ease-in-out hover:scale-105"
              >
                Save
              </button>
              {!isCreateMode && (
                <button
                  type="button"
                  onClick={handleDelete}
                  className="bg-transparent border border-red-400 text-red-400 px-4 py-2 rounded-lg transition duration-300 ease-in-out hover:underline hover:scale-105"
                >
                  Delete
                </button>
              )}
            </div>
          </form>
        </div>
        <div className="flex flex-col items-center mb-4 pt-4"></div>
      </div>
    </div>
  );
};

export default ContactPopupEditForm;
