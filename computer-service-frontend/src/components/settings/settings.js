import React from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { MDBTable, MDBTableBody } from "mdb-react-ui-kit";
import { Button, Col, Row } from "react-bootstrap";

const Settings = () => {
  const { user: currentUser } = useSelector((state) => state.auth);
  const userData = currentUser.data.userData;
  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  return (
    <div className="container-fluid bd-content user-content table-content mt-5">
      <h2 className="content-header">User</h2>
      <MDBTable className="table mt-5" hover>
        <MDBTableBody>
          <tr className="table-row">
            <td className="table-element-title">First Name</td>
            <td className="table-element-data">{userData.firstName}</td>
          </tr>
          <tr className="table-row">
            <td className="table-element-title">Last Name</td>
            <td className="table-element-data">{userData.lastName}</td>
          </tr>
          <tr className="table-row">
            <td className="table-element-title">Email</td>
            <td className="table-element-data">{userData.email}</td>
          </tr>
          <tr className="table-row">
            <td className="table-element-title">Phone Number</td>
            <td className="table-element-data">{userData.phoneNumber}</td>
          </tr>
          <tr className="table-row">
            <td className="table-element-title">Role</td>
            <td className="table-element-data">{userData.role}</td>
          </tr>
        </MDBTableBody>
      </MDBTable>
      <Row className="user-buttons">
        <Col className="edit-data-col">
        <Button className="edit-data-btn">
          Edit Data
        </Button>
        </Col>
      </Row>
    </div>
  );
};

export default Settings;
