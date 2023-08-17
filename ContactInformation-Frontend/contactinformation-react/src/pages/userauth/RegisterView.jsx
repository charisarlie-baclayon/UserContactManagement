import React from "react";
import MainButton from "../../components/MainButton";
import { useNavigate } from "react-router-dom";

const RegisterView = () => {
  const navigate = useNavigate();
  return (
    <section className="bg-mainBlack min-h-screen flex items-center justify-center">
      <div className="bg-darkBlack flex rounded-xl shadow-lg max-w-3xl">
        <div className="flex justify-center p-5">
          <div className="px-16 p-2">
            <h2 className="text-accentPurple font-bold text-2xl text-center">
              Register
            </h2>
            <p className="text-whiteText text-sm mt-4 text-center">
              If you don't have an account yet, register now.
            </p>
            <form action="" className="flex flex-col gap-4 pb-5 mt-4 ">
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
                    required
                  />
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
                    required
                  />
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
                  required
                />
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
                    required
                  />
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
                    required
                  />
                </div>
              </div>
              <hr class="border-none "></hr>
              <div className="flex justify-between">
                <button className="m-2 w-1/2 bg-accentPurple rounded text-darkBlack py-2 font-bold transition duration-300 ease-in-out hover:underline hover:scale-105">
                  Register
                </button>
                <button
                  className="m-2 w-1/2 bg-transparent border border-accentPurple rounded text-whiteText py-2 font-bold transition duration-300 ease-in-out hover:underline hover:scale-105"
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
