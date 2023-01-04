import axios from "axios";
import { Navigate } from "react-router-dom";
import AuthHeader from "../auth/auth-header";

const API_URL = process.env.REACT_APP_API_URL + "authentication";

const getCurrentUser = () => {
  return JSON.parse(sessionStorage.getItem("user"));
};

async function register(firstName, lastName, email, password, phoneNumber, role) {
  const registeredUserId = await axios.post(
    process.env.REACT_APP_API_URL + "users",
    {
      email: email,
      firstName: firstName,
      lastName: lastName,
      password,
      phoneNumber,
      role,
    },
    { headers: AuthHeader() }
  ).then((response) => {
    console.log(response.data)
    return response.data.userId;
  })
  sessionStorage.setItem("RegisteredUserId", registeredUserId)
};

const login = (email, password) => {
  return axios
    .post(API_URL, null, {
      params: {
        email,
        password,
      },
    })
    .then((response) => {
      if (response.data) {
        sessionStorage.setItem("user", JSON.stringify(response.data));
      }
      return response.data;
    });
};

const logout = () => {
  sessionStorage.clear();
};

const AuthService = {
  register,
  login,
  logout,
  getCurrentUser,
};

export default AuthService;
