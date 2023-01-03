import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import axios from "axios";
import AuthHeader from "../../services/auth/auth-header";
import OrderTable from "./order-table";

const API_URL = process.env.REACT_APP_API_URL + "orders/";

const EditOrder = () => {
  const navigate = useNavigate();
  const { user: currentUser } = useSelector((state) => state.auth);
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const currentOrder = useParams().orderId;

  const fetchOrder = async (orderId = currentOrder) => {
    setLoading(true);
    await axios
      .get(API_URL + `${orderId}`, {
        headers: AuthHeader(),
      })
      .then((response) => {
        setData(response.data.data);
        setLoading(false);
      })
      .catch((err) => {
        console.log(err);
        if (err.code === "ERR_BAD_REQUEST") {
          navigate("/not-found");
          window.location.reload();
        }
      });
  };

  useEffect(() => {
    if (!currentUser) navigate("/login");
    fetchOrder(currentOrder);
  });

  return (
  <div className="container-fluid bd-content mt-5">
  <h2 className="content-header">Order</h2>
  <OrderTable />
  </div>
  );
};

export default EditOrder;
