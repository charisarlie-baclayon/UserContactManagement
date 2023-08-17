import React, { useState, useEffect } from "react";
import ContactHeader from "./ContactHeader";
import { getContacts, updateContact } from "../api/contact/apiContact";
import ContactAddButton from "./ContactAddButton";
import ContactPopup from "../components/ContactPopup";
import ContactPopupEditForm from "./ContactPopupEditForm";

const ContactList = (props) => {
  const token = sessionStorage.getItem("key");
  const [contacts, setContacts] = useState([]);
  const [selectedContact, setSelectedContact] = useState(null); // Track selected contact
  const [isPopupOpen, setIsPopupOpen] = useState(false); // Manage popup visibility
  const [isAddButtonClicked, setIsAddButtonClicked] = useState(false);

  useEffect(() => {
    async function fetchContacts() {
      try {
        const response = await getContacts();
        console.log(response.data);
        setContacts(response.data);
      } catch (error) {
        console.error("Error fetching contacts:", error);
      }
    }

    fetchContacts();
  }, []);

  const filteredContacts = contacts.filter(
    (contact) =>
      props.filterPredicate(contact) &&
      `${contact.firstName} ${contact.lastName}`
        .toLowerCase()
        .includes(props.searchTerm.toLowerCase())
  );

  const openPopup = (contact) => {
    setSelectedContact(contact);
    setIsPopupOpen(true);
  };

  const closePopup = () => {
    setSelectedContact(null);
    setIsPopupOpen(false);
  };
  const closePopupOnAdd = () => {
    setIsAddButtonClicked(false);
    closePopup();
  };

  const handleAddButtonClick = () => {
    setIsAddButtonClicked(true);
    openPopup(null); // Open the popup with a new contact object
  };

  return (
    <>
      <ContactHeader
        searchTerm={props.searchTerm}
        setSearchTerm={props.setSearchTerm}
        title={props.title}
      />

      {filteredContacts.length === 0 ? (
        <p className="text-center mt-4 text-gray-500">No contacts found.</p>
      ) : (
        <ul role="list" className="divide-y divide-greyBorder">
          {filteredContacts.map((contact) => (
            <li
              key={contact.emailAddress}
              className="flex justify-between gap-x-6 py-5 cursor-pointer hover:bg-accentPurple rounded-md"
              onClick={() => openPopup(contact)}
            >
              <div className="flex min-w-0 gap-x-4">
                <img
                  className="h-12 w-12 flex-none rounded-full"
                  src="../src/assets/people.png"
                  alt=""
                />
                <div className="min-w-0 flex-auto">
                  <p className="text-sm font-semibold leading-6 text-whiterText">
                    {contact.firstName} {contact.lastName}
                  </p>
                  <p className="mt-1 truncate text-xs leading-5 text-whiteText">
                    {contact.emailAddress}
                  </p>
                </div>
              </div>
            </li>
          ))}
        </ul>
      )}

      {isPopupOpen && selectedContact && (
        <ContactPopup
          selectedContact={selectedContact}
          closePopup={closePopup}
        />
      )}

      <ContactAddButton onClick={handleAddButtonClick} />

      {isAddButtonClicked && (
        <ContactPopupEditForm
          selectedContact={""}
          isCreateMode={true} // Pass a prop to indicate create mode
          closePopup={closePopupOnAdd}
        />
      )}
    </>
  );
};

export default ContactList;
