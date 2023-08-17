import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

const Sidebar = () => {
  const [open, setOpen] = useState(false);
  const navigate = useNavigate();
  const Menus = [
    { title: "Contacts", src: "contact.svg", route: "/" },
    { title: "Favorites", src: "favorite.svg", route: "/favorites" },
    { title: "Settings", src: "settings.svg", route: "/settings" },
  ];
  return (
    <div
      className={`${open ? "w-72" : "w-20"} 
      p-5 pt-8 duration-300 bg-mainBlack border-r-2 border-greyBorder sticky top-0 h-screen `}
    >
      <div
        className="flex gap-x-4 items-center cursor-pointer"
        onClick={() => setOpen(!open)}
      >
        <img
          src="/contactlogo.png"
          className={`w-8 cursor-pointer duration-500 ${
            open && "rotate-[360deg]"
          }`}
        ></img>
        <h1
          className={`text-whiterText origin-left font-medium text-2xl duration-300 ${
            !open && "scale-0"
          }`}
        >
          Contact
        </h1>
      </div>
      <div className="pt-4">
        <ul>
          {Menus.map((menu, index) => (
            <li
              key={index}
              className={`text-whiterText text-sm flex items-center gap-x-4 pt-4 pb-4 pl-1 cursor-pointer 
      hover:bg-accentPurple rounded-md`}
              onClick={() => navigate(menu.route)}
            >
              <img
                src={`./src/assets/${menu.src}`}
                className="w-8"
                alt={menu.title}
              />
              <span
                className={`${
                  !open && "hidden"
                } origin-left duration-500 text-xl`}
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
