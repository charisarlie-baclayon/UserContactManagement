import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { getUser, updateUser, deleteUser } from "../../api/user/apiUser"; // Import the getUser, updateUser, and deleteUser methods from your API file

const UserSettingsView = () => {
  const navigate = useNavigate();
  const [user, setUser] = useState({
    firstName: "",
    lastName: "",
    username: "",
    email: "",
    password: "",
    confirmPassword: "",
  });

  const [validationErrors, setValidationErrors] = useState({
    firstName: "",
    lastName: "",
    username: "",
    email: "",
    password: "",
    confirmPassword: "",
    success: "",
  });

  const validate = () => {
    const errors = {};
    if (!user.firstName) {
      errors.firstName = "First name is required.";
    } else if (user.firstName.length < 2 || user.firstName.length > 50) {
      errors.firstName = "First name must be between 2 and 50 characters.";
    }

    if (!user.lastName) {
      errors.lastName = "Last name is required.";
    } else if (user.lastName.length < 2 || user.lastName.length > 50) {
      errors.lastName = "Last name must be between 2 and 50 characters.";
    }

    if (!user.username) {
      errors.username = "Username is required.";
    } else if (user.username.length < 3 || user.username.length > 50) {
      errors.username = "Username must be between 3 and 50 characters.";
    } else if (!/^[a-zA-Z0-9_]+$/.test(user.username)) {
      errors.username =
        "Username can only contain letters, numbers, and underscores.";
    }

    if (!user.email) {
      errors.email = "Email is required.";
    } else if (!/\S+@\S+\.\S+/.test(user.email)) {
      errors.email = "Invalid email address.";
    } else if (user.email.length > 100) {
      errors.email = "Email must not exceed 100 characters.";
    }
    if (!user.password) {
      errors.password = "Password is required.";
    } else if (user.password !== user.confirmPassword) {
      errors.confirmPassword = "Passwords do not match.";
    }

    return Object.keys(errors).length === 0 ? null : errors;
  };

  useEffect(() => {
    async function fetchUser() {
      try {
        const userData = await getUser();
        setUser(userData);
      } catch (error) {
        console.error("Error fetching user:", error);
      }
    }
    fetchUser();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUser((prevUser) => ({ ...prevUser, [name]: value }));
  };

  const handleUpdate = async (e) => {
    e.preventDefault();
    const errors = validate();
    if (errors) {
      setValidationErrors(errors);
    } else {
      setValidationErrors({});
      const confirmed = window.confirm("Are you sure you want to update?");

      if (confirmed) {
        try {
          const updatedUserData = await updateUser(user);
          console.log("User updated:", updatedUserData);
          navigate("/settings");
        } catch (error) {
          if (error.response) {
            if (error.response.status === 404) {
              console.error("User not found:", error.response.data);
              setValidationErrors({
                success: "User not found",
              });
            } else if (
              error.response.status === 500 &&
              error.response.data === "User update failed."
            ) {
              console.error("User update failed:", error.response.data);
              setValidationErrors({
                success: "User update failed.",
              });
            } else {
              console.error("Error updating user:", error.response.data);
            }
          } else {
            console.error("Error updating user:", error);
          }
        }
      }
    }
  };

  const handleDelete = async () => {
    try {
      const confirmed = window.confirm("Are you sure you want to delete?");

      if (confirmed) {
        await deleteUser();
        handleLogout();
      }
    } catch (error) {
      if (error.response) {
        if (
          error.response.status === 500 &&
          error.response.data === "User deletion failed."
        ) {
          console.error("User deletion failed:", error.response.data);
          // Handle user deletion failed error (UserDeletionFailedException)
        } else {
          console.error("Error deleting user:", error.response.data);
          // Handle other errors
        }
      } else {
        console.error("Error deleting user:", error);
        // Handle other errors
      }
    }
  };

  const handleLogout = () => {
    // Clear session storage or any authentication token
    sessionStorage.removeItem("key");
    // You can add any additional logout logic here, e.g., redirect to login
    navigate("/login");
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-semibold mb-4 text-whiterText">
        User Settings
      </h1>
      <div className="pl-20 pr-20">
        <div className="bg-gray-800 p-6 rounded-lg shadow-md">
          <h2 className="text-xl font-semibold mb-2 text-center text-whiterText">
            Profile Information
          </h2>
          <form onSubmit={handleUpdate}>
            <div className="mb-4">
              <label className="text-sm block text-gray-300">First Name</label>
              <input
                type="text"
                name="firstName"
                className="text-md w-full p-2 rounded-md bg-gray-700 text-white"
                value={user.firstName}
                onChange={handleChange}
              />
              <div className="text-red-500 text-sm">
                {validationErrors.firstName}
              </div>
            </div>
            <div className="mb-4">
              <label className="text-sm block text-gray-300">Last Name</label>
              <input
                type="text"
                name="lastName"
                className="text-md w-full p-2 rounded-md bg-gray-700 text-white"
                value={user.lastName}
                onChange={handleChange}
              />
              <div className="text-red-500 text-sm">
                {validationErrors.lastName}
              </div>
            </div>
            <div className="mb-4">
              <label className="text-sm block text-gray-300">Username</label>
              <input
                type="text"
                name="username"
                className="text-md w-full p-2 rounded-md bg-gray-700 text-white"
                value={user.username}
                onChange={handleChange}
              />
              <div className="text-red-500 text-sm">
                {validationErrors.username}
              </div>
            </div>
            <div className="mb-4">
              <label className="text-sm block text-gray-300">Email</label>
              <input
                type="email"
                name="email"
                className="text-md w-full p-2 rounded-md bg-gray-700 text-white"
                value={user.email}
                onChange={handleChange}
              />
              <div className="text-red-500 text-sm">
                {validationErrors.email}
              </div>
            </div>
            <div className="flex gap-4">
              <div className="mb-4 w-1/2">
                <label className="text-sm block text-gray-300">Password</label>
                <input
                  type="password"
                  name="password"
                  className="text-md w-full p-2 rounded-md bg-gray-700 text-white"
                  value={user.password}
                  onChange={handleChange}
                />
                <div className="text-sm text-red-500">
                  {validationErrors.password}
                </div>
              </div>
              <div className="mb-4 w-1/2">
                <label className="text-sm block text-gray-300">
                  Confirm Password
                </label>
                <input
                  type="password"
                  name="confirmPassword"
                  className="text-md w-full p-2 rounded-md bg-gray-700 text-white"
                  value={user.confirmPassword}
                  onChange={handleChange}
                />
                <div className="text-red-500 text-sm">
                  {validationErrors.confirmPassword}
                </div>
              </div>
            </div>
            <div className="flex justify-between">
              <button
                onClick={handleDelete}
                className="text-md bg-red-500 px-4 py-2 text-white rounded-lg hover:bg-red-600"
              >
                Delete Account
              </button>

              <button
                type="submit"
                className="bg-darkerPurple text-white py-2 px-4 rounded-lg hover:bg-purple-600"
              >
                Save Changes
              </button>
            </div>
          </form>
        </div>
        <div className="mt-6 flex justify-between">
          <button
            className="text-darkerPurple hover:underline"
            onClick={handleLogout}
          >
            Logout
          </button>
          <Link to="/" className="text-darkerPurple hover:underline">
            Back to Home
          </Link>
        </div>
      </div>
    </div>
  );
};

export default UserSettingsView;
