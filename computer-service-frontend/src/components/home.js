import React from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";

const Home = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  return (
    <div className="main-content">
      Home
      {sessionStorage.getItem("user")}
    </div>
  );
};

export default Home;