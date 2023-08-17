import React, { useState } from "react";
import ContactPopup from "../components/ContactPopup";
import ContactPopupEditForm from "../components/ContactPopupEditForm";

const ContactPopupContainer = (props) => {
  const [isEditing, setIsEditing] = useState(false);

  const handleEditClick = () => {
    setIsEditing(true);
  };

  const handleClosePopup = () => {
    setIsEditing(false);
  };

  return (
    <div>
      {isEditing ? (
        <ContactPopupEditForm
          selectedContact={props.selectedContact}
          closePopup={handleClosePopup}
        />
      ) : (
        <ContactPopup
          selectedContact={props.selectedContact}
          openEditForm={handleEditClick}
        />
      )}
    </div>
  );
};

export default ContactPopupContainer;
