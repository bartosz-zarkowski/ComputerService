import axios from "axios";
import authHeader from "../auth/auth-header";
import { Navigate } from "react-router-dom";

const API_URL = process.env.REACT_APP_API_URL + "orderDetails";

async function GetOrderDetailsAsync(orderId) {
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

const OrderDetailsService = {
  GetOrderDetailsAsync,
};

export default OrderDetailsService;
