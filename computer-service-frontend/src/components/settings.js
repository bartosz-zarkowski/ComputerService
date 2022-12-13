import React from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";

const Settings = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  return <div>Settings</div>;
};

export default Settings;