import axios from "axios";
import authHeader from "../auth/auth-header";
import AsyncStorage from "@react-native-async-storage/async-storage";

const API_URL = process.env.REACT_APP_API_URL + "accessories";

const setEmptyAccessoriesToLocalStorage = () => {
  localStorage.setItem(
    "@Accesories",
    JSON.stringify([
      {
        AccessoryId: "",
        name: "",
      },
    ])
  );
};

async function setEmptyAccesosryToLocalStorageAsync() {
  await AsyncStorage.setItem(
    "@Accesories",
    JSON.stringify([
      {
        AccessoryId: "",
        name: "",
      },
    ])
  );
}

const GetStoredAccesory = () => {
  var storedAccessiories = localStorage.getItem("@Accesories");
  if (storedAccessiories) {
    return JSON.parse(localStorage.getItem("@Accesories"));
  } else {
    setEmptyAccessoriesToLocalStorage();
    return JSON.parse(localStorage.getItem("@Accesories"));
  }
};

async function GetStoredAccessoryAsync() {
  var storedAccessiories = await AsyncStorage.getItem("@Accesories");
  if (storedAccessiories) {
    return JSON.parse(storedAccessiories);
  } else {
    await setEmptyAccesosryToLocalStorageAsync();
    return JSON.parse(await AsyncStorage.getItem("@Accesories"));
  }
}

const SetStoredAccessory = (AccessoryId, name) => {
  localStorage.setItem(
    "@Accesories",
    JSON.stringify({
      AccessoryId: AccessoryId,
      name: name,
    })
  );
};

async function SetStoredAccessoryAsync(AccessoryId, name) {
  await AsyncStorage.setItem(
    "@Accesories",
    JSON.stringify({
      AccessoryId: AccessoryId,
      name: name,
    })
  );
}

async function GetAccesoriesAsync() {
  await axios.get(API_URL, { headers: authHeader() }).then((response) => {
    if (response.data)
      AsyncStorage.setItem("@Accesories", JSON.stringify(response.data));
    return response;
  });
}

async function CreateAccessoryAsync() {
  const accessory = await GetStoredAccessoryAsync();
  return axios
    .post(
      API_URL,
      {
        name: accessory.name,
      },
      { headers: authHeader() }
    )
    .then((response) => {
      if (response.data) {
        SetStoredAccessory();
      }
    });
}

const AccessoryService = {
  GetStoredAccesory,
  GetStoredAccessoryAsync,
  SetStoredAccessory,
  SetStoredAccessoryAsync,
  GetAccesoriesAsync,
  CreateAccessoryAsync,
};

export default AccessoryService;
