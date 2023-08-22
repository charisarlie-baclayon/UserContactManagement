import React, { useState, useEffect } from "react";
import MainButton from "../../components/MainButton";
import loginPhoto from "../../assets/loginphoto.jpg";
import { useNavigate } from "react-router-dom";
import { loginUser } from "../../api/auth/apiAuth";

const LoginView = () => {
  const navigate = useNavigate();
  const [loginData, setLoginData] = useState({
    username: "",
    password: "",
  });
  const [validationErrors, setValidationErrors] = useState({
    username: "",
    password: "",
    success: "",
  });

  const validate = () => {
    const errors = {};
    if (!loginData.username) {
      errors.username = "Username is required.";
    }
    if (!loginData.password) {
      errors.password = "Password is required.";
    }
    return Object.keys(errors).length === 0 ? null : errors;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setLoginData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    const errors = validate();
    if (errors) {
      setValidationErrors(errors);
    } else {
      const errors = {
        username: "",
        password: "",
      };
      setValidationErrors(errors);
      try {
        const response = await loginUser(
          loginData.username,
          loginData.password
        );

        if (response.status === 200) {
          const token = response.data;
          console.log(token);
          sessionStorage.setItem("key", token);
          navigate("/");
        }
      } catch (error) {
      if (error.response) {
        if (error.response.status === 401) {
          // Unauthorized (Invalid credentials)
          setValidationErrors({
            success: "Invalid username or password.",
          });
        } else if (error.response.status === 500) {
          // Server error
          console.error("Server error:", error.response.data);
        }
      } else {
        console.error("Error during login:", error);
      }
    }
  }
  };

  useEffect(() => {
    const sessionKey = sessionStorage.getItem("key");

    if (sessionKey) {
      navigate("/");
    }
  }, []);

  return (
    <>
      <section className="bg-mainBlack min-h-screen flex items-center justify-center">
        <div className="bg-darkBlack flex rounded-xl shadow-lg max-w-3xl">
          <div className="flex flex-col justify-center sm:w-1/2 p-5">
            <div className="px-16 p-2">
              <h2 className="text-darkerPurple font-bold text-2xl text-center">
                Login
              </h2>
              <p className="text-whiteText text-sm mt-4 text-center">
                If you already a member, easily login.
              </p>
              <form
                onSubmit={handleLogin}
                action=""
                className="flex flex-col gap-4"
              >
                <div className="mt-8">
                  <label htmlFor="username" className="text-whiterText mt-8">
                    Username
                  </label>
                  <input
                    className="p-2 rounded border w-full bg-wh"
                    type="text"
                    name="username"
                    placeholder="username"
                    value={loginData.username}
                    onChange={handleChange}
                  ></input>
                  <div className="text-red-500 text-sm">
                    {validationErrors.username}
                  </div>
                </div>

                <div>
                  <label htmlFor="password" className="text-whiterText">
                    Password
                  </label>
                  <input
                    className="p-2 rounded border w-full bg-wh"
                    type="password"
                    name="password"
                    placeholder="password"
                    value={loginData.password}
                    onChange={handleChange}
                  ></input>
                  <div className="text-red-500 text-sm">
                    {validationErrors.password}
                  </div>
                </div>

                <MainButton text="Login"></MainButton>
                <div className="text-red-500 text-sm">
                  {validationErrors.success}
                </div>
              </form>
              <div className="mt-10 grid grid-cols-3 items-center text-whiteText">
                <hr className="border-whiteText"></hr>
                <p className="text-center">OR</p>
                <hr className="border-whiteText"></hr>
              </div>

              <p className="text-whiteText text-sm mt-4 text-center pt-2 pb-2">
                Make an account.
              </p>
              <button
                className="my-2 bg-transparent border border-darkerPurple rounded text-whiteText py-2 w-full transition duration-300 ease-in-out hover:underline hover:scale-105"
                onClick={() => navigate("/register")}
              >
                Register
              </button>
            </div>
          </div>
          <div className="sm:block hidden w-1/2 relative">
            <img className="rounded-e-xl" src={loginPhoto} alt="" />
            <div className="absolute inset-0 bg-gradient-to-br from-darkerPurple to-darkerPurple opacity-30 rounded-xl"></div>
          </div>
        </div>
      </section>
    </>
  );
};

export default LoginView;
