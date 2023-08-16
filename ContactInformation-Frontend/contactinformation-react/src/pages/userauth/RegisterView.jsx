import React from "react";

const RegisterView = () => {
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
            <form action="" className="flex flex-col gap-4 pb-5">
              <input
                className="p-2 mt-8 rounded border"
                type="text"
                name="firstName"
                placeholder="First Name"
                required
              />
              <input
                className="p-2 rounded border"
                type="text"
                name="lastName"
                placeholder="Last Name"
                required
              />
              <input
                className="p-2 rounded border"
                type="text"
                name="username"
                placeholder="Username"
                required
              />
              <input
                className="p-2 rounded border"
                type="password"
                name="password"
                placeholder="Password"
                required
              />
              <input
                className="p-2 rounded border"
                type="password"
                name="confirmPassword"
                placeholder="Confirm Password"
                required
              />
              <button className="bg-accentPurple rounded text-darkBlack py-2 font-bold transition duration-300 ease-in-out hover:underline hover:scale-105">
                Register
              </button>
            </form>
          </div>
        </div>
      </div>
    </section>
  );
};

export default RegisterView;
