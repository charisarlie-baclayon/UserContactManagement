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
        if (selectedAddressIndex !== -1) {
          // Update existing address
          console.log(props.selectedAddress);
          const updatedAddress = addresses[selectedAddressIndex];
          await updateAddress(
            props.contactId,
            updatedAddress.id,
            updatedAddress
          );
        } else {
          // Create new addresses
          for (const address of addresses) {
            await createAddress(props.contactId, address);
          }
        }
        // Handle successful form submission
        console.log("Addresses submitted:", addresses);
        window.location.reload();
        props.closePopup();
      } catch (error) {
        console.log(error);
      }
    }
  };
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
        await deleteAddress(props.contactId, addressId);
        console.log("Address deleted:", addressId);

        // Update the addresses state to remove the deleted address
        const updatedAddresses = addresses.filter(
          (address) => address.id !== addressId
        );
        setAddresses(updatedAddresses);
      } catch (error) {
        console.log(error);
      }
    }
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
