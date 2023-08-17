import axios from "axios";

const API_BASE_URL = "http://localhost:5173";

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

export const loginUser = async (userLoginDto) => {
  try {
    const response = await axios.post(
      `${API_BASE_URL}/api/authentication/login`,
      userLoginDto
    );
    console.log(response);
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};
