import React from "react";

const Login = () => {
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
                <div class="relative">
                  <input
                    class="p-2 rounded border w-full"
                    type="password"
                    name="password"
                    placeholder="password"
                  ></input>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    width="16"
                    height="16"
                    fill="gray"
                    class="bi bi-eye absolute top-1/2 right-3 -translate-y-1/2"
                    viewBox="0 0 16 16"
                  >
                    <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z" />
                    <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z" />
                  </svg>
                </div>
                <button class="bg-accentPurple rounded text-darkBlack py-2 font-bold">
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
              <button class="my-2 bg-transparent border border-accentPurple rounded text-whiteText py-2 w-full">
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

export default Login;
