import React, { useState, useEffect } from "react";
import ContactHeader from "../../components/ContactHeader";
import { getContacts } from "../../api/contact/apiContact";

const FavoriteContactListView = () => {
  const token = sessionStorage.getItem("key");
  const [searchTerm, setSearchTerm] = useState("");
  const [contacts, setContacts] = useState([]);

  useEffect(() => {
    async function fetchContacts() {
      try {
        const response = await getContacts(token);
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
      contact.favorite &&
      `${contact.firstName} ${contact.lastName}`
        .toLowerCase()
        .includes(searchTerm.toLowerCase())
  );

  return (
    <>
      <ContactHeader
        searchTerm={searchTerm}
        setSearchTerm={setSearchTerm}
        title="Your Favorites"
      />

      <ul role="list" className="divide-y divide-greyBorder">
        {filteredContacts.map((contact) => (
          <li
            key={contact.emailAddress}
            className="flex justify-between gap-x-6 py-5"
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
    </>
  );
};

export default FavoriteContactListView;
