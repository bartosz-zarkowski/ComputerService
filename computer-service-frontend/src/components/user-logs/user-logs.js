import React from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import RolesService from "../../services/auth/roles";
import LogsTable from "./logs-table";

const UserLogs = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  if (!RolesService.isAdmin()) {
    return <Navigate to="/home" />;
  }

  return (
    <div className="main-content">
      <LogsTable />
    </div>
  );
};

export default UserLogs;
