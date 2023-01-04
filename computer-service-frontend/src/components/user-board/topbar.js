import { React, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { Dropdown } from "react-bootstrap";
import { useSelector, useDispatch } from "react-redux";
import { logout } from "../../actions/auth";

import "../../style/topbar.css";
import { PersonCircle } from "react-bootstrap-icons";

const Topbar = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  const navigate = useNavigate();

  const dispatch = useDispatch();

  const toProfile = () => {
    navigate("/profile");
  }

  const logOut = useCallback(() => {
    dispatch(logout());
  }, [dispatch]);

  return (
    <nav className="topbar navbar navbar-expand navbar-dark">
      <div className="userDropdown rounded">
        <Dropdown>
          <Dropdown.Toggle
            className="userDropdown btn rounded"
            id="dropdown-custom-components "
          >
            <div className="user row rounded">
              <div className="user-name col-6">
                {currentUser.data.userData.firstName}
                <br></br>
                {currentUser.data.userData.lastName}
              </div>
              <div className="user-icon col-6">
                <PersonCircle size={50} />
              </div>
            </div>
          </Dropdown.Toggle>

          <Dropdown.Menu>
            <Dropdown.Item eventKey="1">
              <Dropdown.Item className="header" onClick={toProfile}>Profile</Dropdown.Item>
            </Dropdown.Item>
              <Dropdown.Item eventKey="2">
                  <Dropdown.Item className="header" onClick={logOut}>Log out</Dropdown.Item>
              </Dropdown.Item>
          </Dropdown.Menu>
        </Dropdown>
      </div>
    </nav>
  );
};

export default Topbar;
