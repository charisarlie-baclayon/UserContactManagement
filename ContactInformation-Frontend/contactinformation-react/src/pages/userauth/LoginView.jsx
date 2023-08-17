import React, { useState, useEffect } from "react";
import MainButton from "../../components/MainButton";
import loginPhoto from "../../assets/loginphoto.jpg";
import { useNavigate } from "react-router-dom";
import { loginUser } from "../../api/auth/apiAuth";

const LoginView = () => {
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async (e) => {
    e.preventDefault();
    const response = await loginUser(username, password);
    console.log(response.data);
    sessionStorage.setItem("key", response.data);
    navigate("/");
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
              <h2 className="text-accentPurple font-bold text-2xl text-center">
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
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                  ></input>
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
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                  ></input>
                </div>

                <MainButton text="Login"></MainButton>
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
                className="my-2 bg-transparent border border-accentPurple rounded text-whiteText py-2 w-full transition duration-300 ease-in-out hover:underline hover:scale-105"
                onClick={() => navigate("/register")}
              >
                Register
              </button>
            </div>
          </div>
          <div className="sm:block hidden w-1/2 relative">
            <img className="rounded-e-xl" src={loginPhoto} alt="" />
            <div className="absolute inset-0 bg-gradient-to-br from-accentPurple to-darkerPurple opacity-30 rounded-xl"></div>
          </div>
        </div>
      </section>
    </>
  );
};

export default LoginView;
