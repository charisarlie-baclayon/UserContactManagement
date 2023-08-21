import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { registerUser, loginUser } from "../../api/auth/apiAuth";

const RegisterView = () => {
  const navigate = useNavigate();
  const [registrationData, setRegistrationData] = useState({
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
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setRegistrationData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  useEffect(() => {
    const sessionKey = sessionStorage.getItem("key");

    if (sessionKey) {
      navigate("/");
    }
  }, []);

  const handleRegister = async (e) => {
    e.preventDefault();
    try {
      const errors = validate();
      if (errors) {
        setValidationErrors(errors);
      } else {
        setValidationErrors(null);
        const response = await registerUser(registrationData);
        console.log(response.data);
        handleLogin();
      }
    } catch (error) {
      console.error("Error registering:", error);
    }
  };

  const validate = () => {
    const errors ={}
    if (!registrationData.firstName) {
      errors.firstName = "First name is required.";
    } else if (
      registrationData.firstName.length < 2 ||
      registrationData.firstName.length > 50
    ) {
      errors.firstName = "First name must be between 2 and 50 characters.";
    }

    if (!registrationData.lastName) {
      errors.lastName = "Last name is required.";
    } else if (
      registrationData.lastName.length < 2 ||
      registrationData.lastName.length > 50
    ) {
      errors.lastName = "Last name must be between 2 and 50 characters.";
    }

    if (!registrationData.username) {
      errors.username = "Username is required.";
    } else if (
      registrationData.username.length < 3 ||
      registrationData.username.length > 50
    ) {
      errors.username = "Username must be between 3 and 50 characters.";
    } else if (!/^[a-zA-Z0-9_]+$/.test(registrationData.username)) {
      errors.username =
        "Username can only contain letters, numbers, and underscores.";
    }

    if (!registrationData.email) {
      errors.email = "Email is required.";
    } else if (!/\S+@\S+\.\S+/.test(registrationData.email)) {
      errors.email = "Invalid email address.";
    } else if (registrationData.email.length > 100) {
      errors.email = "Email must not exceed 100 characters.";
    }
    if (!registrationData.password){
      errors.password = "Confirm Password is required.";
    }
    else if (registrationData.password !== registrationData.confirmPassword) {
      errors.confirmPassword = "Passwords do not match.";
    }

    return Object.keys(errors).length === 0? null:errors;
  };


  const handleLogin = async (e) => {
    if (e) {
      e.preventDefault();
    }

    const response = await loginUser(
      registrationData.username,
      registrationData.password
    );

    console.log(response.data);
    sessionStorage.setItem("key", response.data);
    navigate("/");
    window.location.reload();
  };

  return (
    <section className="bg-mainBlack min-h-screen flex items-center justify-center">
      <div className="bg-darkBlack flex rounded-xl shadow-lg max-w-3xl">
        <div className="flex justify-center p-5">
          <div className="px-16 p-2">
            <h2 className="text-darkerPurple font-bold text-2xl text-center">
              Register
            </h2>
            <p className="text-whiteText text-sm mt-4 text-center">
              If you don't have an account yet, register now.
            </p>
            <form
              action=""
              onSubmit={handleRegister}
              className="flex flex-col gap-4 pb-5 mt-4 "
            >
              <div className="flex gap-4">
                <div className="w-1/2">
                  <label htmlFor="firstName" className="text-whiterText">
                    First Name
                  </label>
                  <input
                    className="p-2 rounded border w-full"
                    type="text"
                    id="firstName"
                    name="firstName"
                    placeholder="First Name"
                    value={registrationData.firstName}
                    onChange={handleChange}
                  />
                  <div className="text-red-500 text-sm">
                    {validationErrors.firstName}
                  </div>
                </div>
                <div className="w-1/2">
                  <label htmlFor="lastName" className="text-whiterText">
                    Last Name
                  </label>
                  <input
                    className="p-2 rounded border w-full"
                    type="text"
                    id="lastName"
                    name="lastName"
                    placeholder="Last Name"
                    value={registrationData.lastName}
                    onChange={handleChange}
                  />

                  <div className="text-red-500 text-sm">
                    {validationErrors.lastName}
                  </div>
                </div>
              </div>
              <div>
                <label htmlFor="username" className="text-whiterText">
                  Username
                </label>
                <input
                  className="p-2 rounded border w-full"
                  type="text"
                  id="username"
                  name="username"
                  placeholder="Username"
                  value={registrationData.username}
                  onChange={handleChange}
                />

                <div className="text-red-500 text-sm">
                  {validationErrors.username}
                </div>
              </div>
              <div>
                <label htmlFor="email" className="text-whiterText">
                  Email
                </label>
                <input
                  className="p-2 rounded border w-full"
                  type="email"
                  id="email"
                  name="email"
                  placeholder="Email"
                  value={registrationData.email}
                  onChange={handleChange}
                />

                <div className="text-red-500 text-sm">
                  {validationErrors.email}
                </div>
              </div>
              <div className="flex gap-4">
                <div className="w-1/2">
                  <label htmlFor="password" className="text-whiterText">
                    Password
                  </label>
                  <input
                    className="p-2 rounded border w-full"
                    type="password"
                    id="password"
                    name="password"
                    placeholder="Password"
                    value={registrationData.password}
                    onChange={handleChange}
                  />

                  <div className="text-red-500 text-sm">
                    {validationErrors.password}
                  </div>
                </div>
                <div className="w-1/2">
                  <label htmlFor="confirmPassword" className="text-whiterText">
                    Confirm Password
                  </label>
                  <input
                    className="p-2 rounded border w-full"
                    type="password"
                    id="confirmPassword"
                    name="confirmPassword"
                    placeholder="Confirm Password"
                    value={registrationData.confirmPassword}
                    onChange={handleChange}
                  />

                  <div className="text-red-500 text-sm">
                    {validationErrors.confirmPassword}
                  </div>
                </div>
              </div>
              <hr class="border-none "></hr>
              <div className="flex justify-between">
                <button className="m-2 w-1/2 bg-darkerPurple rounded text-darkBlack py-2 font-bold transition duration-300 ease-in-out hover:underline hover:scale-105">
                  Register
                </button>
                <button
                  className="m-2 w-1/2 bg-transparent border border-darkerPurple rounded text-whiteText py-2 font-bold transition duration-300 ease-in-out hover:underline hover:scale-105"
                  onClick={() => navigate("/login")}
                >
                  Cancel
                </button>
              </div>
              <hr class="border-none "></hr>
            </form>
          </div>
        </div>
      </div>
    </section>
  );
};

export default RegisterView;
