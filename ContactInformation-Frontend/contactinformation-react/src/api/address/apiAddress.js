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

export const getAddresses = async (contactId) => {
  try {
    const response = await api.get(`/api/contacts/${contactId}/addresses`);
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const createAddress = async (contactId, addressData) => {
  try {
    const response = await api.post(
      `/api/contacts/${contactId}/addresses`,
      addressData
    );
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const updateAddress = async (contactId, addressId, addressData) => {
  try {
    const response = await api.put(
      `/api/contacts/${contactId}/addresses/${addressId}`,
      addressData
    );
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const deleteAddress = async (contactId, addressId) => {
  try {
    const response = await api.delete(
      `/api/contacts/${contactId}/addresses/${addressId}`
    );
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const getAddressTypes = async (contactId) => {
  try {
    const response = await api.get(
      `/api/contacts/${contactId}/addresses/address-types`
    );
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export default api;
