import React, { useState, useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import AppRoutes from "../app-routes";
import { logout } from "../../actions/auth";

import "bootstrap/dist/css/bootstrap.min.css";
import "../../style/sidebar.css";

import {
  HouseDoorFill,
  PlusCircleFill,
  ListColumnsReverse,
  PencilSquare,
  PeopleFill,
  PersonPlusFill,
  PersonBadgeFill,
} from "react-bootstrap-icons";
import AuthVerify from "../../services/auth/auth-verify";
import Topbar from "./topbar";

const UserBoard = () => {
  const [showReceiverBoard, setShowReceiverBoard] = useState(false);
  const [showAdminBoard, setShowAdminBoard] = useState(false);
  const [showTechnicianBoard, setShowTechnicianBoard] = useState(false);

  const { user: currentUser } = useSelector((state) => state.auth);

  const dispatch = useDispatch();

  const logOut = useCallback(() => {
    dispatch(logout());
  }, [dispatch]);

  useEffect(() => {
    if (currentUser) {
      setShowTechnicianBoard(
        currentUser.data.userData.role.includes("Technician")
      );
      setShowReceiverBoard(currentUser.data.userData.role.includes("Receiver"));
      setShowAdminBoard(
        currentUser.data.userData.role.includes("Administrator")
      );
    } else {
      setShowTechnicianBoard(false);
      setShowReceiverBoard(false);
      setShowAdminBoard(false);
    }
  }, [currentUser, showAdminBoard, showReceiverBoard, showTechnicianBoard]);

  return (
    <div className="container-fluid px-0 mx-0">
      <Topbar />
      <div className="wrapper d-flex" id="wrapper">
        <div className="sidebar border-end" id="sidebar-wrapper">
          <div className="list-group list-group-flush px-3 pt-3 pb-3">
            <Link
              to={"/home"}
              className="list-group-item list-group-item-action p-3 rounded"
            >
              <div className="nav-item row">
                <div className="col-3">
                  <HouseDoorFill size={30} />
                </div>
                <div className="col-9 mt-1">Home</div>
              </div>
            </Link>

            {!showTechnicianBoard && (
              <Link
                to={"/create-order"}
                className="list-group-item list-group-item-action p-3 rounded"
              >
                <div className="nav-item row">
                  <div className="col-3">
                    <PlusCircleFill size={30} />
                  </div>
                  <div className="col-9 mt-1">Create Order</div>
                </div>
              </Link>
            )}

            <Link
              to={"/orders"}
              className="list-group-item list-group-item-action p-3 rounded"
            >
              <div className="nav-item row">
                <div className="col-3">
                  <ListColumnsReverse size={30} />
                </div>
                <div className="col-9 mt-1">Orders</div>
              </div>
            </Link>

            {!showTechnicianBoard && (
              <Link
                to={"/customers"}
                className="list-group-item list-group-item-action p-3 rounded"
              >
                <div className="row">
                  <div className="col-3">
                    <PeopleFill size={30} />
                  </div>
                  <div className="col-9 mt-1">Customers</div>
                </div>
              </Link>
            )}

            {showAdminBoard && (
              <Link
                to={"/register"}
                className="list-group-item list-group-item-action p-3 rounded"
              >
                <div className="nav-item row">
                  <div className="nav-logo col-3">
                    <PersonPlusFill size={30} />
                  </div>
                  <div className="col-9 mt-1">Register User</div>
                </div>
              </Link>
            )}

            {showAdminBoard && (
              <Link
                to={"/users"}
                className="list-group-item list-group-item-action p-3 rounded"
              >
                <div className="nav-item row">
                  <div className="col-3">
                    <PersonBadgeFill size={30} />
                  </div>
                  <div className="col-9 mt-1">Users</div>
                </div>
              </Link>
            )}

            {showAdminBoard && (
              <Link
                to={"/user-logs"}
                className="list-group-item list-group-item-action p-3 rounded"
              >
                <div className="nav-item row">
                  <div className="col-3">
                    <PencilSquare size={30} />
                  </div>
                  <div className="col-9 mt-1">User Logs</div>
                </div>
              </Link>
            )}
          </div>
        </div>
        <div className="container-fluid bd-content mt-5">
          <AppRoutes />
        </div>
        <AuthVerify logOut={logOut} />
      </div>
    </div>
  );
};

export default UserBoard;
