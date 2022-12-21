import React from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { MDBTable, MDBTableBody } from 'mdb-react-ui-kit';
import { Card } from "react-bootstrap";

const Settings = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  return (
    <div className="container-fluid bd-content mt-5">
    <MDBTable hover>
    <MDBTableBody>
      <tr>
        <th scope='row'></th>
        <td>First Name</td>
        <td>{currentUser.data.userData.firstName}</td>
      </tr>
      <tr>
        <th scope='row'></th>
        <td>Last Name</td>
        <td>{currentUser.data.userData.lastName}</td>
      </tr>
      <tr>
        <th scope='row'></th>
        <td>Email</td>
        <td>{currentUser.data.userData.email}</td>
      </tr>
      <tr>
        <th scope='row'></th>
        <td>Phone Number</td>
        <td>{currentUser.data.userData.phoneNumber}</td>
      </tr>
      <tr>
        <th scope='row'></th>
        <td>Role</td>
        <td>{currentUser.data.userData.role}</td>
      </tr>
    </MDBTableBody>
  </MDBTable>
  </div>
  );
};

export default Settings;