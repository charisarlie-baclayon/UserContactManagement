import axios from "axios";

const API_BASE_URL = "http://localhost:7038";

const api = axios.create({
  baseURL: API_BASE_URL,
});

api.interceptors.request.use((config) => {
  const token = sessionStorage.getItem("key");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const getContacts = async () => {
  try {
    const response = await api.get("/api/contacts");
    return response;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const createContact = async (contactData) => {
  try {
    const response = await api.post("/api/contacts", contactData);
    return response;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const updateContact = async (contactId, contactData) => {
  try {
    const response = await api.put(`/api/contacts/${contactId}`, contactData);
    return response;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const deleteContact = async (contactId) => {
  try {
    const response = await api.delete(`/api/contacts/${contactId}`);
    return response;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export default api;
