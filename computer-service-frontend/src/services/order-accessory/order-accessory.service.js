import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "axios";
import authHeader from "../auth/auth-header";
import OrderService from "../order/order.service";

const API_URL = process.env.REACT_APP_API_URL + "orderAccessories";

const setEmptyOrderAccessoriesToLocalStorage = () => {
  localStorage.setItem("@OrderAccessories", JSON.stringify([]));
};

async function setEmptyOrderAccessoriesToLocalStorageAsync() {
  await AsyncStorage.setItem("@OrderAccessories", JSON.stringify([]));
}

const GetStoredOrderAccessories = () => {
  var storedOrderAccessories = localStorage.getItem(
    "@OrderAccessories" || "[]"
  );
  if (!storedOrderAccessories) setEmptyOrderAccessoriesToLocalStorage();
  return JSON.parse(localStorage.getItem("@OrderAccessories" || "[]"));
};

async function GetStoredOrderAccessoriesAsync() {
  var storedOrderAccessories = await AsyncStorage.getItem(
    "@OrderAccessories" || "[]"
  );
  if (!storedOrderAccessories)
    await setEmptyOrderAccessoriesToLocalStorageAsync();
  return JSON.parse(await AsyncStorage.getItem("@OrderAccessories" || "[]"));
}

const SetStoredOrderAccessories = (orderAccessoryName) => {
  var orderAccessories = GetStoredOrderAccessories();

  orderAccessories.push({ id: "", name: orderAccessoryName });
  localStorage.setItem("@OrderAccessories", JSON.stringify(orderAccessories));
};

async function CreateOrerAccessoriesAsync() {
  const orderAccesories = await GetStoredOrderAccessoriesAsync();
  const order = await OrderService.GetStoredOrderAsync();
  orderAccesories.forEach((orderAccessory) => {
    axios.post(
      API_URL,
      { 
        Name: orderAccessory.name,
        OrderId: order.orderId
      },
      { headers: authHeader() }
    );
  });
}

const OrderAccessoryService = {
  GetStoredOrderAccessories,
  GetStoredOrderAccessoriesAsync,
  SetStoredOrderAccessories,
  CreateOrerAccessoriesAsync,
};

export default OrderAccessoryService;
