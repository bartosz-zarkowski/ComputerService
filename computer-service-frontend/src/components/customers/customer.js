import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import axios from "axios";
import AuthHeader from "../../services/auth/auth-header";

const API_URL = process.env.REACT_APP_API_URL + "customers/";

const User = () => {
  const navigate = useNavigate();
  const { user: currentUser } = useSelector((state) => state.auth);
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const currentCustomerId = useParams().customerId;

  const fetchCustomer = async (customerId = currentCustomerId) => {
    setLoading(true);

    await axios
      .get(API_URL + `${customerId}`, {
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
    fetchCustomer(currentCustomerId);
  }, []);

  return (
    <div className="main-content">
      Customer Page <br />
      Customer Id: {currentCustomerId}
    </div>
  );
};

export default User;
