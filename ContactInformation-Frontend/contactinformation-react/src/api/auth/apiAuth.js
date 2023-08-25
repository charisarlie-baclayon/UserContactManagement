import axios from "axios";

const API_BASE_URL = "http://localhost:7038/";

// Create a base axios instance with common settings
const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
});

// Interceptor to handle errors globally
axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    console.log(error);
    throw error;
  }
);

export const registerUser = async (userRegistrationDto) => {
  try {
    const response = await axiosInstance.post(
      "/api/authentication/register",
      userRegistrationDto
    );
    return response;
  } catch (error) {
    throw error;
  }
};

export const loginUser = async (username, password) => {
  try {
    const response = await axiosInstance.post("/api/authentication/login", {
      username,
      password,
    });
    return response;
  } catch (error) {
    throw error;
  }
};
