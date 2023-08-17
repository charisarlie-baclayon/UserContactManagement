import axios from "axios";

const API_BASE_URL = "https://localhost:7038/";

export const registerUser = async (userRegistrationDto) => {
  try {
    const response = await axios.post(
      `${API_BASE_URL}/api/authentication/register`,
      userRegistrationDto
    );
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const loginUser = async (username, password) => {
  try {
    const response = await axios.post(
      `${API_BASE_URL}api/authentication/login`,
      { username: username, password: password }
    );
    console.log(response);
    return response;
  } catch (error) {
    console.log(error);
    throw error;
  }
};
