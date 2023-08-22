import React, { useState, useEffect } from "react";
import {
  getAddressTypes,
  createAddress,
  updateAddress,
  deleteAddress,
} from "../api/address/apiAddress";

const AddressForm = (props) => {
  const [addresses, setAddresses] = useState([
    { id: "", addressDescription: "", addressType: "" },
  ]);
  const [addressTypes, setAddressTypes] = useState([]);

  const [selectedAddressIndex, setSelectedAddressIndex] = useState(-1);

  const [validationErrors, setValidationErrors] = useState({
    addressDescription: "",
    addressType: "",
    success: "",
  });

  const validate = (address) => {
    const errors = {};
    if (!address.addressDescription) {
      errors.addressDescription = "Address description is required.";
    } else if (address.addressDescription.length < 10) {
      errors.addressDescription =
        "Address description must be at least 10 characters.";
    }

    if (!address.addressType) {
      errors.addressType = "Address type is required.";
    }

    return Object.keys(errors).length === 0 ? null : errors;
  };

  useEffect(() => {
    // Fetch address types when the component mounts
    fetchAddressTypes();
  }, []);

  useEffect(() => {
    // Set the selected address data as default when selectedAddress changes
    if (props.selectedAddress) {
      setAddresses([props.selectedAddress]);
      setSelectedAddressIndex(0);
    }
  }, [props.selectedAddress]);

  const fetchAddressTypes = async () => {
    try {
      const types = await getAddressTypes(props.contactId);
      setAddressTypes(types);
    } catch (error) {
      console.log(error);
    }
  };

  const handleInputChange = (event, index) => {
    const { name, value } = event.target;
    const updatedAddresses = addresses.map((address, i) =>
      i === index ? { ...address, [name]: value } : address
    );
    setAddresses(updatedAddresses);
  };

  const handleFormSubmit = async (event) => {
    event.preventDefault();

    const confirmed = window.confirm(
      "Are you sure you want to submit this address?"
    );

    if (confirmed) {
      try {
        let allValid = true;

        for (const address of addresses) {
          const errors = validate(address);

          if (errors) {
            setValidationErrors(errors);
            allValid = false;
            break; // No need to continue checking if any address is invalid
          }
        }

        if (allValid) {
          setValidationErrors({});

          if (selectedAddressIndex !== -1) {
            // Update existing address
            const updatedAddress = addresses[selectedAddressIndex];

            try {
              const response = await updateAddress(
                props.contactId,
                updatedAddress.id,
                updatedAddress
              );

              if (response.status === 200) {
                console.log("Address updated:", response.data);
                // Handle successful form submission
                window.location.reload();
                props.closePopup();
              }
            } catch (error) {
              handleApiError(error);
            }
          } else {
            // Create new addresses
            try {
              for (const address of addresses) {
                const response = await createAddress(props.contactId, address);

                if (response.status === 200) {
                  console.log("Address created:", response.data);
                } else {
                  console.error("Unexpected response:", response);
                }
              }

              // Handle successful form submission
              window.location.reload();
              props.closePopup();
            } catch (error) {
              handleApiError(error);
            }
          }
          window.location.reload();
          props.closePopup();
        }
      } catch (error) {
        console.log("An error occurred:", error);
        setValidationErrors({
          success: "Something went wrong.",
        });
      }
    }
  };

  const handleApiError = (error) => {
    if (error.response) {
      if (error.response.status === 404) {
        console.error("Address or contact not found.");
        setValidationErrors({
          success: "Address or contact not found.",
        });
      } else if (error.response.status === 400) {
        console.error("Bad request:", error.response.data);
        setValidationErrors({
          success:
            "Update failed. Something went wrong. Please check your data.",
        });
      } else {
        console.error("Server error:", error.response.data);
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
  };

  // const handleFormSubmit = async (event) => {
  //   event.preventDefault();

  //   const confirmed = window.confirm(
  //     "Are you sure you want to submit this address?"
  //   );

  //   if (confirmed) {
  //     try {
  //       if (selectedAddressIndex !== -1) {
  //         // Update existing address
  //         console.log(props.selectedAddress);
  //         const updatedAddress = addresses[selectedAddressIndex];

  //         try {
  //           const errors = validate(updateAddress);

  //           if (errors) {
  //             setValidationErrors(errors);
  //           } else {
  //             setValidationErrors({});

  //             const response = await updateAddress(
  //               props.contactId,
  //               updatedAddress.id,
  //               updatedAddress
  //             );

  //             if (response.status === 200) {
  //               console.log("Address updated:", response.data);
  //               // Handle successful form submission
  //               window.location.reload();
  //               props.closePopup();
  //             }
  //           }
  //         } catch (error) {
  //           if (error.response) {
  //             if (error.response.status === 404) {
  //               console.error("Address or contact not found.");
  //               setValidationErrors({
  //                 success: "Address or contact not found.",
  //               });
  //             } else if (error.response.status === 400) {
  //               console.error("Bad request:", error.response.data);
  //               setValidationErrors({
  //                 success:
  //                   "Update failed. Something went wrong. Please check you data.",
  //               });
  //             } else {
  //               console.error("Server error:", error.response.data);
  //               setValidationErrors({
  //                 success: "Something went wrong.",
  //               });
  //             }
  //           } else {
  //             console.error("An error occurred:", error);
  //             setValidationErrors({
  //               success: "Something went wrong.",
  //             });
  //           }
  //         }
  //       } else {
  //         // Create new addresses
  //         try {
  //           for (const address of addresses) {
  //             const errors = validate(address);

  //             if (errors) {
  //               setValidationErrors(errors);
  //             } else {
  //               setValidationErrors({});
  //               const response = await createAddress(props.contactId, address);

  //               if (response.status === 200) {
  //                 console.log("Address created:", response.data);
  //                 // Handle successful form submission
  //                 window.location.reload();
  //                 props.closePopup();
  //               }
  //             }
  //           }
  //         } catch (error) {
  //           if (error.response) {
  //             if (error.response.status === 404) {
  //               console.log("Contact not found.");
  //               setValidationErrors({
  //                 success: "Contact does not exist.",
  //               });
  //             } else if (error.response.status === 400) {
  //               console.log("Bad request:", error.response.data);
  //               setValidationErrors({
  //                 success:
  //                   "Create failed. Something went wrong. Please check you data.",
  //               });
  //             } else {
  //               console.log("Server error:", error.response.data);
  //               setValidationErrors({
  //                 success: "Something went wrong.",
  //               });
  //             }
  //           } else {
  //             console.log("An error occurred:", error);
  //             setValidationErrors({
  //               success: "Something went wrong.",
  //             });
  //           }
  //         }
  //       }
  //     } catch (error) {
  //       console.log("An error occurred:", error);
  //       setValidationErrors({
  //         success: "Something went wrong.",
  //       });
  //     }
  //   }
  //   window.location.reload();
  //   props.closePopup();
  // };

  const handleEditAddressClick = (index) => {
    setSelectedAddressIndex(index);
    setAddresses((prevAddresses) =>
      prevAddresses.map((address, i) =>
        i === index ? { ...selectedAddress } : address
      )
    );
  };

  const handleDeleteAddress = async (addressId) => {
    const confirmed = window.confirm(
      "Are you sure you want to delete this address?"
    );

    if (confirmed) {
      try {
        const response = await deleteAddress(props.contactId, addressId);

        if (response.status === 200) {
          console.log("Address deleted:", addressId);

          // Update the addresses state to remove the deleted address
          const updatedAddresses = addresses.filter(
            (address) => address.id !== addressId
          );
          setAddresses(updatedAddresses);
        }
      } catch (error) {
        if (error.response) {
          if (error.response.status === 404) {
            console.error("Address or contact not found.");
            setValidationErrors({
              success: "Address or contact does not exist.",
            });
          } else if (error.response.status === 400) {
            console.error("Bad request:", error.response.data);
            setValidationErrors({
              success: "Bad request. Please check again.",
            });
          } else {
            console.error("Server error:", error.response.data);
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
    window.location.reload();
    props.closePopup();
  };

  const addNewAddress = () => {
    setAddresses([...addresses, { addressDescription: "", addressType: "" }]);
  };
  const isEditMode = selectedAddressIndex !== -1;
  const buttonText = isEditMode ? "Save" : "Add";

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
            {addresses.map((address, index) => (
              <div key={index} className="flex flex-col gap-y-2">
                <div>
                  <span className="text-sm">Address Description:</span>
                  <div className="rounded-md">
                    <textarea
                      type="text"
                      name="addressDescription"
                      value={address.addressDescription}
                      onChange={(event) => handleInputChange(event, index)}
                      className="text-lg text-whiterText p-1 bg-gray-700 rounded-md w-full resize-none"
                      rows={3}
                    />
                    <div className="text-red-500 text-sm">
                      {validationErrors.addressDescription}
                    </div>
                  </div>
                </div>
                <div>
                  <span className="text-sm">Address Type:</span>
                  <div className="rounded-md">
                    <select
                      name="addressType"
                      value={address.addressType}
                      onChange={(event) => handleInputChange(event, index)}
                      className="text-lg text-whiterText p-1 bg-gray-700 rounded-md w-full"
                    >
                      <option value="">Select an address type</option>
                      {addressTypes.map((type) => (
                        <option key={type} value={type}>
                          {type}
                        </option>
                      ))}
                    </select>
                    <div className="text-red-500 text-sm">
                      {validationErrors.addressType}
                    </div>
                  </div>
                </div>
                <div></div>
                {isEditMode && (
                  <button
                    type="button"
                    onClick={() => handleDeleteAddress(address.id)}
                    className="bg-transparent border border-red-400 text-red-400 px-4 py-2 rounded-lg transition duration-300 ease-in-out hover:underline hover:scale-105"
                  >
                    Delete
                  </button>
                )}
              </div>
            ))}
            <div className="flex flex-col gap-y-4 mt-2">
              <button
                type="submit"
                className="bg-darkerPurple text-white px-4 py-2 rounded-lg transition duration-300 ease-in-out hover:scale-105"
              >
                {isEditMode ? "Save" : "Add"}
              </button>
            </div>
          </form>
        </div>
        <div className="flex flex-col items-center mb-4 pt-4"></div>
      </div>
    </div>
  );
};

export default AddressForm;
