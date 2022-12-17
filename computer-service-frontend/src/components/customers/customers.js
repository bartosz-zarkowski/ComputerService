import React, { useState, useEffect } from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import RolesService from "../../services/auth/roles";
import CustomersTable from "./customers-table";

const Customers = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  return (
    <div className="main-content">
      <h2 className="content-header">Customers</h2>
      <CustomersTable />
    </div>
  );
};

export default Customers;
