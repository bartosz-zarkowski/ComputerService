import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { MDBTable, MDBTableBody } from 'mdb-react-ui-kit';

const Settings = () => {
  const { user: currentUser } = useSelector((state) => state.auth);
  const navigate = useNavigate();

  useEffect(() => {
    if (!currentUser) {
      navigate("/login");
      window.location.reload();
    }
  }, []);

  return (
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
  );
};

export default Settings;