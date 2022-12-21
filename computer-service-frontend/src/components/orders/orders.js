import React from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import OrdersTable from "./orders-table";

const CreateOrder = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  return (
    <div className="container-fluid bd-content mt-5">
      <h2 className="content-header">Orders</h2>
      <OrdersTable />
    </div>
  );
};

export default CreateOrder;
