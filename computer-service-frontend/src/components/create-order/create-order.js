import { React } from "react";
import { useSelector } from "react-redux";
import { Navigate } from "react-router-dom";

import RolesService from "../../services/auth/roles";
import OrderForm from "./order-form";


const CreateOrder = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  if (!RolesService.isAdmin()) {
    return <Navigate to="/home" />;
  }

  return (
    <div className="container-fluid bd-content mt-5">
      <h2 className="content-header header">Create Order</h2>
      <OrderForm />
    </div>
  );
};

export default CreateOrder;
