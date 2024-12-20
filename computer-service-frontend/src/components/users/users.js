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
    <div className="container-fluid bd-content mt-5">
      <h2 className="content-header">Users</h2>
      <UsersTable />
    </div>
  );
};

export default Users;
