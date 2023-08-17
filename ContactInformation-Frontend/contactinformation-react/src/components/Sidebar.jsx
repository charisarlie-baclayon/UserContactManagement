import React, { useState } from "react";
import arrow from "../assets/arrow.svg";

const Sidebar = () => {
  const [open, setOpen] = useState(false);
  const Menus = [
    { title: "Contacts", src: "contact.svg" },
    { title: "Favorites", src: "favorite.svg" },
    { title: "Settings", src: "settings.svg" },
  ];
  return (
    <div
      class={`${open ? "w-72" : "w-20"} 
      p-5 pt-8 duration-300 bg-mainBlack border-r-2 border-greyBorder sticky top-0 h-screen `}
    >
      <div class="flex gap-x-4 items-center">
        <img
          src="/contactlogo.png"
          class={`w-8 cursor-pointer duration-500 ${open && "rotate-[360deg]"}`}
          onClick={() => setOpen(!open)}
        ></img>
        <h1
          class={`text-whiterText origin-left font-medium text-2xl duration-300 ${
            !open && "scale-0"
          }`}
        >
          Contact
        </h1>
      </div>
      <div class="pt-4">
        <ul>
          {Menus.map((menu, index) => (
            <li
              key={index}
              class={`text-whiterText text-sm flex items-center gap-x-4 pt-4 pb-4 pl-1 cursor-pointer 
              hover:bg-accentPurple rounded-md`}
            >
              <img src={`./src/assets/${menu.src}`} class="w-8"></img>
              <span
                class={`${!open && "hidden"} origin-left duration-500 text-xl`}
              >
                {menu.title}
              </span>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default Sidebar;
