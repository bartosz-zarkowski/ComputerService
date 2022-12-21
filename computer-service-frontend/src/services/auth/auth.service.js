import axios from "axios";
import AuthHeader from "../auth/auth-header";

const API_URL = process.env.REACT_APP_API_URL + "authentication";

const getCurrentUser = () => {
  return JSON.parse(sessionStorage.getItem("user"));
};

const register = (firstName, lastName, email, password, phoneNumber, role) => {
  return axios.post(
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
  );
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
  localStorage.clear();
};

const AuthService = {
  register,
  login,
  logout,
  getCurrentUser,
};

export default AuthService;
