import React from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";

const Home = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  return (
    <div className="container-fluid bd-content mt-5">
      <div className="home-content">
      <h2 className="content-header">Home</h2>
      {sessionStorage.getItem("user")}
      </div>
    </div>
  );
};

export default Home;