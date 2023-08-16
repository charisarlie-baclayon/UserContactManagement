/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{html,js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        mainBlack: "#16181e",
        darkBlack: "#000104",
        whiteText: "#fefcfc",
        accentPurple: "#e859e9",
        darkerPurple: "#390e53"
      },
    },
  },
  plugins: [],
};
