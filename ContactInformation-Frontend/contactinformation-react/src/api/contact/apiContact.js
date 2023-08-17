import axios from "axios";

const API_BASE_URL = "https://localhost:7038";

export const getContacts = async (token) => {
  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  };
  try {
    const response = await axios.get(`${API_BASE_URL}/api/contacts`, config);
    console.log(response);
    return response;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const createContact = async (contactData) => {
  try {
    const response = await axios.post(
      `${API_BASE_URL}/api/contacts`,
      contactData
    );
    console.log(response);
    return response;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const updateContact = async (contactId, contactData) => {
  try {
    const response = await axios.put(
      `${API_BASE_URL}/api/contacts/${contactId}`,
      contactData
    );
    console.log(response);
    return response;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const deleteContact = async (contactId) => {
  try {
    const response = await axios.delete(
      `${API_BASE_URL}/api/contacts/${contactId}`
    );
    console.log(response);
    return response;
  } catch (error) {
    console.log(error);
    throw error;
  }
};
