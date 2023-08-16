import React from "react";

const LoginView = () => {
  return (
    <>
      <section class="bg-mainBlack min-h-screen flex items-center justify-center">
        <div class="bg-darkBlack flex rounded-xl shadow-lg max-w-3xl">
          <div class="flex flex-col justify-center sm:w-1/2 p-5">
            <div class="px-16 p-2">
              <h2 class="text-accentPurple font-bold text-2xl text-center">
                Login
              </h2>
              <p class="text-whiteText text-sm mt-4 text-center">
                If you already a member, easily login.
              </p>
              <form action="" class="flex flex-col gap-4">
                <input
                  class="p-2 mt-8 rounded border"
                  type="text"
                  name="username"
                  placeholder="username"
                ></input>
                <input
                  class="p-2 rounded border w-full"
                  type="password"
                  name="password"
                  placeholder="password"
                ></input>
                <button class="bg-accentPurple rounded text-darkBlack py-2 font-bold transition duration-300 ease-in-out hover:underline hover:scale-105">
                  Login
                </button>
              </form>
              <div class="mt-10 grid grid-cols-3 items-center text-whiteText">
                <hr class="border-whiteText"></hr>
                <p class="text-center">OR</p>
                <hr class="border-whiteText"></hr>
              </div>

              <p class="text-whiteText text-sm mt-4 text-center pt-2 pb-2">
                Make an account.
              </p>
              <button class="my-2 bg-transparent border border-accentPurple rounded text-whiteText py-2 w-full transition duration-300 ease-in-out hover:underline hover:scale-105">
                Register
              </button>
            </div>
          </div>
          <div class="sm:block hidden w-1/2 relative">
            <img class="rounded-e-xl" src="loginphoto.jpg" alt="" />
            <div class="absolute inset-0 bg-gradient-to-br from-accentPurple to-darkerPurple opacity-30 rounded-xl"></div>
          </div>
        </div>
      </section>
    </>
  );
};

export default LoginView;
