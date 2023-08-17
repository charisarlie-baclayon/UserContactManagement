import React, { useState } from "react";
import ContactList from "../../components/ContactList";

const ContactListView = () => {
  const [searchTerm, setSearchTerm] = useState("");

  return (
    <ContactList
      searchTerm={searchTerm}
      setSearchTerm={setSearchTerm}
      title="Your Contacts"
      filterPredicate={() => true} // No specific filtering in ContactListView
    />
  );
};

export default ContactListView;
