import axios from "axios";
import authHeader from "../auth/auth-header";
import AsyncStorage from "@react-native-async-storage/async-storage";
import CustomerService from "../customer/customer.service";
import DeviceService from "../device/device.service";
import OrderAccessoryService from "../order-accessory/order-accessory.service";
import AddressService from "../address/address-service";
import { Navigate } from "react-router-dom";

const API_URL = process.env.REACT_APP_API_URL + "orders";

const setEmptyOrderToStorage = () => {
  localStorage.setItem(
    "@order",
    JSON.stringify({ orderId: "", customerId: "", title: "", description: "" })
  );
};

async function setEmptyOrderToStorageAsync() {
  await AsyncStorage.setItem(
    "@order",
    JSON.stringify({ orderId: "", customerId: "", title: "", description: "" })
  );
}

const GetStoredOrder = () => {
  var storedOrder = localStorage.getItem("@order");
  if (storedOrder) {
    return JSON.parse(storedOrder);
  } else {
    setEmptyOrderToStorage();
    return JSON.parse(localStorage.getItem("@order"));
  }
};

async function GetStoredOrderAsync() {
  var storedOrder = await AsyncStorage.getItem("@order");
  if (storedOrder) {
    return JSON.parse(storedOrder);
  } else {
    await setEmptyOrderToStorageAsync();
    return JSON.parse(await AsyncStorage.getItem("@order"));
  }
}

const SetStoredOrder = (orderId, customerId, title, description) => {
  localStorage.setItem(
    "@order",
    JSON.stringify({
      orderId: orderId,
      customerId: customerId,
      title: title,
      description: description,
    })
  );
};

async function SetStoredOrderAsync(orderId, customerId, title, description) {
  await AsyncStorage.setItem(
    "@order",
    JSON.stringify({
      orderId: orderId,
      customerId: customerId,
      title: title,
      description: description,
    })
  );
}

async function GetOrderAsync(orderId) {
  return await axios
    .get(API_URL + `/${orderId}`, {
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

async function createOrderAsync() {
  const order = await GetStoredOrderAsync();
  const customer = await CustomerService.GetStoredCustomerAsync();
  const customerId = customer.customerId;
  return await axios
    .post(
      API_URL,
      {
        CustomerId: customerId,
        Title: order.title,
        Description: order.description,
      },
      { headers: authHeader() }
    )
    .then((response) => {
      if (response.data) {
        AsyncStorage.setItem(
          "@order",
          JSON.stringify({
            orderId: response.data.orderId,
            customerId: customerId,
            title: order.title,
            description: order.description,
          })
        );
        sessionStorage.setItem("CreatedOrderId", response.data.orderId);
      }
    });
}

async function removeStorageAsync() {
  const clearStorage = await AsyncStorage.multiRemove(["@order", "@customer", "@OrderAccessories", "@OrderDevice", "@Address"]);
  Promise.resolve(clearStorage);
}

async function CreateOrderAsync() {
  const createCustomer = await CustomerService.CreateCustomerAsync();
  const createAddress = await AddressService.CreateAddressAsync();
  const createOrder = await createOrderAsync();
  const createDevice = await DeviceService.CreateOrderDeviceAsync();
  const createOrderAccessories = await OrderAccessoryService.CreateOrerAccessoriesAsync();
  const removeStorage = await removeStorageAsync();
  Promise.all([
    createCustomer,
    createAddress,
    createOrder,
    createDevice,
    removeStorage,
    createOrderAccessories
  ]);
}

const OrderService = {
  GetStoredOrder,
  createOrderAsync,
  CreateOrderAsync,
  SetStoredOrder,
  GetStoredOrderAsync,
  SetStoredOrderAsync,
  GetOrderAsync,
  removeStorageAsync
};

export default OrderService;
