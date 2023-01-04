import React, { useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Navigate, useLocation } from "react-router-dom";

import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";
import { clearMessage } from "./actions/message";
import AppRoutes from "./components/app-routes";
import UserBoard from "./components/user-board/user-board";
import { logout } from "./actions/auth";
import { Modal } from "react-bootstrap";

const App = () => {
  const { user: currentUser } = useSelector((state) => state.auth);
  const dispatch = useDispatch();

  let location = useLocation();

  useEffect(() => {
    if (["/login"].includes(location.pathname)) {
      dispatch(clearMessage());
    }
  }, [dispatch, location]);

  const logOut = useCallback(() => {
    dispatch(logout());
  }, [dispatch]);

  if (currentUser) {
    return (
      <div className="whole-content container-fluid px-0">
      <UserBoard />
      </div>
    );
  }

  if (!currentUser) {
    return (
      <AppRoutes />
    );
  }
}

export default App;
