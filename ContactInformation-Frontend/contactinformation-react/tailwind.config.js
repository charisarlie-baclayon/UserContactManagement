/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{html,js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        mainBlack: "#16181e",
        darkBlack: "#000104",
        whiterText: "#fefcfc",
        whiteText: "#9ca3af",
        greyBorder: "#4b5563",
        accentPurple: "#390e53",
        darkerPurple: "#e859e9",
        midPurple: "#642E8F",
      },
    },
  },
  plugins: [],
};
