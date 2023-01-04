import React, { useState, useEffect } from "react";
import { Navigate, useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import { MDBTable, MDBTableBody } from "mdb-react-ui-kit";
import { Button, Col, Row } from "react-bootstrap";

import UserService from "../../services/user/user.service";

import "../../style/table.css";
import "../../style/user.css";
import RolesService from "../../services/auth/roles";

const User = () => {
  const { user: currentUser } = useSelector((state) => state.auth);
  const [dataFetched, setDataFetched] = useState(false);
  const userId = useParams().userId;
  const [user, setUser] = useState("");

  const dateTimeOffSetToString = (date) => {
    return new Date(date).toLocaleString([], {
      year: "numeric",
      month: "2-digit",
      day: "2-digit",
      hour: "2-digit",
      minute: "2-digit",
    });
  };

  useEffect(() => {
    async function getData() {
      const user = await UserService.GetUserAsync(userId);
      setUser(user);
      setDataFetched(true);
    }
    if (!user) {
      getData();
    }
  });

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  if (!RolesService.isAdmin()) {
    return <Navigate to="/home" />;
  }

  if (dataFetched) {
    return (
      <div className="container-fluid bd-content table-content mt-5">
        <h2 className="content-header">User</h2>
        <MDBTable className="table mt-5" hover>
          <MDBTableBody>
            <tr className="table-row">
              <td className="table-element-title">First Name</td>
              <td className="table-element-data">{user.firstName}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Last Name</td>
              <td className="table-element-data">{user.lastName}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Email</td>
              <td className="table-element-data">{user.email}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Phone Number</td>
              <td className="table-element-data">{user.phoneNumber}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Role</td>
              <td className="table-element-data">{user.role}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Active</td>
              <td className="table-element-data">{`${user.isActive}`}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Created At</td>
              <td className="table-element-data">{dateTimeOffSetToString(user.createdAt)}</td>
            </tr>
          </MDBTableBody>
        </MDBTable>
        <Row className="user-buttons">
          <Col className="delete-user-col">
            <Button className="danger delete-user-btn" variant="danger">
              Delete User
            </Button>{" "}
          </Col>
          <Col className="edit-user-col">
          <Button className="edit-user-btn">
            Edit User
          </Button>
          </Col>
        </Row>
      </div>
    );
  } else {
    return (
      <div className="container-fluid bd-content table-content mt-5">
        <h2 className="content-header">User</h2>
        <div className="loading mt-5">Loading...</div>
    </div>
    );
  }
};

export default User;
