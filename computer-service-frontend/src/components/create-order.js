import React, { useState, useEffect } from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import RolesService from "../services/auth/roles";

const CreateOrder = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  if (RolesService.isTechnician()) {
    return <Navigate to="/home" />;
  }

  return <div>Create Order</div>;
};

export default CreateOrder;