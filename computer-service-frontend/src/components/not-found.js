import React from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import "../style/not-found.css";

const NotFound = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  return (
    <div class="not-found-content d-flex align-items-center justify-content-center">
      <div class="text-center">
        <h1 class="display-1 fw-bold">404</h1>
        <p class="fs-3">
          {" "}
          <span class="text-danger">Opps!</span> Page not found.
        </p>
        <p class="lead">The page you’re looking for doesn’t exist.</p>
      </div>
    </div>
  );
};

export default NotFound;
