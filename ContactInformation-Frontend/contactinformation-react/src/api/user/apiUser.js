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

export const getUser = async () => {
  try {
    const response = await api.get("/api/users");
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const updateUser = async (userToUpdate) => {
  try {
    const response = await api.put("/api/users", userToUpdate);
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const deleteUser = async () => {
  try {
    const response = await api.delete("/api/users");
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export default api;
