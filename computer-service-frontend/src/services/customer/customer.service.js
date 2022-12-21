import axios from "axios";
import authHeader from "../auth/auth-header";
import AsyncStorage from "@react-native-async-storage/async-storage";

const API_URL = process.env.REACT_APP_API_URL + "customers";

const setEmptyCustomerToLocalStorage = () => {
  localStorage.setItem(
    "@customer",
    JSON.stringify({
      customerId: "",
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
    })
  );
};

async function setEmptyCustomerToLocalStorageAsync() {
  await AsyncStorage.setItem(
    "@customer",
    JSON.stringify({
      customerId: "",
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
    })
  );
}

const GetStoredCustomer = () => {
  var storedCustomer = localStorage.getItem("@customer");
  if (storedCustomer) {
    return JSON.parse(localStorage.getItem("@customer"));
  } else {
    setEmptyCustomerToLocalStorage();
    return JSON.parse(localStorage.getItem("@customer"));
  }
};

async function GetStoredCustomerAsync() {
  var storedCustomer = await AsyncStorage.getItem("@customer");
  if (storedCustomer) {
    return JSON.parse(storedCustomer);
  } else {
    await setEmptyCustomerToLocalStorageAsync();
    return JSON.parse(await AsyncStorage.getItem("@customer"));
  }
}

const SetStoredCustomer = (
  customerId,
  customerFirstName,
  customerLastName,
  customerEmail,
  customerPhoneNumber
) => {
  localStorage.setItem(
    "@customer",
    JSON.stringify({
      customerId: customerId,
      firstName: customerFirstName,
      lastName: customerLastName,
      email: customerEmail,
      phoneNumber: customerPhoneNumber,
    })
  );
};

async function SetStoredCustomerAsync(
  customerId,
  customerFirstName,
  customerLastName,
  customerEmail,
  customerPhoneNumber
) {
  await AsyncStorage.setItem(
    "@customer",
    JSON.stringify({
      customerId: customerId,
      firstName: customerFirstName,
      lastName: customerLastName,
      email: customerEmail,
      phoneNumber: customerPhoneNumber,
    })
  );
}

async function GetCustomersAsync() {
  await axios.get(API_URL, { headers: authHeader() }).then((response) => {
    if (response.data)
      AsyncStorage.setItem("@customers", JSON.stringify(response.data));
    return response;
  });
}

async function CreateCustomerAsync() {
  const customer = await GetStoredCustomerAsync();
  return axios
    .post(
      API_URL,
      {
        email: customer.email,
        firstName: customer.firstName,
        lastName: customer.lastName,
        phoneNumber: customer.phoneNumber,
      },
      { headers: authHeader() }
    )
    .then((response) => {
      if (response.data) {
        AsyncStorage.setItem(
          "@customer",
          JSON.stringify({
            customerId: response.data.customerId,
            firstName: customer.firstName,
            lastName: customer.lastName,
            email: customer.email,
            phoneNumber: customer.phoneNumber,
          })
        );
      }
    });
}

const CustomerService = {
  GetStoredCustomer,
  GetStoredCustomerAsync,
  GetCustomersAsync,
  SetStoredCustomer,
  SetStoredCustomerAsync,
  CreateCustomerAsync,
};

export default CustomerService;
