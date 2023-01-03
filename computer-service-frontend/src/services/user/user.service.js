import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "axios";
import { Navigate } from "react-router-dom";
import authHeader from "../auth/auth-header";

const API_URL = process.env.REACT_APP_API_URL + "users";

const setEmptyRegisteredUserToLocalStorage = () => {
  localStorage.setItem(
    "@RegisteredUser",
    JSON.stringify({
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      role: "Technician"
    })
  );
};

async function setEmptyRegisteredUserToLocalStorageAsync() {
  await AsyncStorage.setItem(
    "@RegisteredUser",
    JSON.stringify({
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      role: "Technician"
    })
  );
}

const GetStoredRegisteredUser = () => {
  var registeredUser = localStorage.getItem("@RegisteredUser");
  if (registeredUser) {
    return JSON.parse(localStorage.getItem("@RegisteredUser"));
  } else {
    setEmptyRegisteredUserToLocalStorage();
    return JSON.parse(localStorage.getItem("@RegisteredUser"));
  }
};

async function GetStoredRegisteredUserAsync() {
  var registeredUser = await AsyncStorage.getItem("@RegisteredUser");
  if (registeredUser) {
    return JSON.parse(await AsyncStorage.getItem("@RegisteredUser"));
  } else {
    await setEmptyRegisteredUserToLocalStorageAsync();
    return JSON.parse(await AsyncStorage.getItem("@RegisteredUser"));
  }
};

const SetRegisteredUser = (
  firstName,
  lastName,
  email,
  phoneNumber,
  role
) => {
  localStorage.setItem(
    "@RegisteredUser",
    JSON.stringify({
      firstName: firstName,
      lastName: lastName,
      email: email,
      phoneNumber: phoneNumber,
      role: role
    })
  );
};

async function SetRegisteredUserAsync(
  firstName,
  lastName,
  email,
  phoneNumber,
  role
) {
  await AsyncStorage.setItem(
    "@customer",
    JSON.stringify({
      firstName: firstName,
      lastName: lastName,
      email: email,
      phoneNumber: phoneNumber,
      role: role,
    })
  );
}

async function RemoveStoredRegisteredUserAsync() {
  const clearStorage = await AsyncStorage.multiRemove(["@RegisteredUser"]);
  Promise.resolve(clearStorage);
}

const GetUsers = () => {
    axios.get(API_URL, { headers: authHeader() }).then((response) => {
      if (response.data)
        sessionStorage.setItem("users", JSON.stringify(response.data));
      return response;
    });
  }

  async function GetUserAsync(userId) {
    return await axios
      .get(API_URL + `/${userId}`, {
        headers: authHeader(),
      })
      .then((response) => {
        return response.data.data;
      })
      .catch((err) => {
        console.log(err);
        if (err.code === "ERR_BAD_REQUEST") {
          Navigate("/not-found");
        }
      });
  };

const UserService = {
  GetStoredRegisteredUser,
  GetStoredRegisteredUserAsync,
  SetRegisteredUser,
  SetRegisteredUserAsync,
  RemoveStoredRegisteredUserAsync,
  GetUsers,
  GetUserAsync,
}

export default UserService;