import React, { useState } from "react";
import ContactList from "../../components/ContactList";

const FavoriteContactListView = () => {
  const [searchTerm, setSearchTerm] = useState("");

  return (
    <ContactList
      searchTerm={searchTerm}
      setSearchTerm={setSearchTerm}
      title="Your Favorites"
      filterPredicate={(contact) => contact.favorite}
    />
  );
};

export default FavoriteContactListView;
