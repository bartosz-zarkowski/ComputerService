import axios from "axios";
import authHeader from "../auth/auth-header";
import AsyncStorage from "@react-native-async-storage/async-storage";
import OrderService from "../order/order.service";

const API_URL = process.env.REACT_APP_API_URL + "devices";

const setEmptyOrderDeviceToLocalStorage = () => {
  localStorage.setItem(
    "@OrderDevice",
    JSON.stringify({
      deviceId: "",
      name: "",
      serialNumber: "",
      password: "",
      condition: "",
      hasWarranty: false,
      customerId: "",
      orderId: "",
    })
  );
};

async function setEmptyOrderDeviceToLocalStorageAsync() {
  await AsyncStorage.setItem(
    "@OrderDevice",
    JSON.stringify({
      deviceId: "",
      name: "",
      serialNumber: "",
      password: "",
      condition: "",
      hasWarranty: false,
      customerId: "",
      orderId: "",
    })
  );
}

const GetStoredOrderDevice = () => {
  var storedOrderDevice = localStorage.getItem("@OrderDevice");
  if (storedOrderDevice) {
    return JSON.parse(localStorage.getItem("@OrderDevice"));
  } else {
    setEmptyOrderDeviceToLocalStorage();
    return JSON.parse(localStorage.getItem("@OrderDevice"));
  }
};

async function GetStoredOrderDeviceAsync() {
  var storedOrderDevice = await AsyncStorage.getItem("@OrderDevice");
  if (storedOrderDevice) {
    return JSON.parse(storedOrderDevice);
  } else {
    await setEmptyOrderDeviceToLocalStorageAsync();
    return JSON.parse(await AsyncStorage.getItem("@OrderDevice"));
  }
}

const SetStoredOrderDevice = (
  deviceId,
  name,
  serialNumber,
  password,
  condition,
  hasWarranty,
  customerId,
  orderId
) => {
  localStorage.setItem(
    "@OrderDevice",
    JSON.stringify({
      deviceId: deviceId,
      name: name,
      serialNumber: serialNumber,
      password: password,
      condition: condition,
      hasWarranty: hasWarranty,
      customerId: customerId,
      orderId: orderId,
    })
  );
};

async function SetStoredOrderDeviceAsync(
  deviceId,
  name,
  serialNumber,
  password,
  condition,
  hasWarranty,
  customerId,
  orderId
) {
  await AsyncStorage.setItem(
    "@OrderDevice",
    JSON.stringify({
      deviceId: deviceId,
      name: name,
      serialNumber: serialNumber,
      password: password,
      condition: condition,
      hasWarranty: hasWarranty,
      customerId: customerId,
      orderId: orderId,
    })
  );
}

async function GetOrderDeviceAsync() {
  await axios.get(API_URL, { headers: authHeader() }).then((response) => {
    if (response.data)
      AsyncStorage.setItem("@OrderDevice", JSON.stringify(response.data));
    return response;
  });
}

async function CreateOrderDeviceAsync() {
  const order = await OrderService.GetStoredOrderAsync();
  const orderDevice = await GetStoredOrderDeviceAsync();
  return axios
    .post(
      API_URL,
      {
        deviceId: orderDevice.Id,
        name: orderDevice.name,
        serialNumber: orderDevice.serialNumber,
        password: orderDevice.password,
        condition: orderDevice.condition,
        hasWarranty: orderDevice.hasWarranty,
        customerId: order.customerId,
        orderId: order.orderId,
      },
      { headers: authHeader() }
    )
    .then((response) => {
      if (response.data) {
        SetStoredOrderDeviceAsync(
          orderDevice.Id,
          orderDevice.name,
          orderDevice.serialNumber,
          orderDevice.password,
          orderDevice.condition,
          orderDevice.hasWarranty,
          order.customerId,
          order.orderId
        );
      }
    });
}

const DeviceService = {
  GetStoredOrderDevice,
  GetStoredOrderDeviceAsync,
  GetOrderDeviceAsync,
  CreateOrderDeviceAsync,
  SetStoredOrderDevice,
  SetStoredOrderDeviceAsync,
};

export default DeviceService;
