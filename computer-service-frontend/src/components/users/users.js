import React from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import RolesService from "../../services/auth/roles";
import UsersTable from "./users-table";

const Users = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  if (!RolesService.isAdmin()) {
    return <Navigate to="/home" />;
  }

  return (
    <div className="main-content">
      <h2 className="content-header">Customers</h2>
      <UsersTable />
    </div>
  );
};

export default Users;
