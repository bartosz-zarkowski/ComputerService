import { React, useCallback } from "react";
import { Link } from "react-router-dom";
import { Dropdown } from "react-bootstrap";
import { useSelector, useDispatch } from "react-redux";
import { logout } from "../../actions/auth";

import "../../style/topbar.css";
import { PersonCircle } from "react-bootstrap-icons";

const Topbar = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  const dispatch = useDispatch();

  const logOut = useCallback(() => {
    dispatch(logout());
  }, [dispatch]);

  return (
    <nav className="topbar navbar navbar-expand navbar-dark">
      <Link to={"/login"} className="navbar-brand">
        <img src={require("../../Logo.png")} alt="User Icon" />
      </Link>
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
                {currentUser.data.userData.firstName}
              </div>
              <div className="user-icon col-6">
                <PersonCircle size={50} />
              </div>
            </div>
          </Dropdown.Toggle>

          <Dropdown.Menu>
            <Dropdown.Item eventKey="1">
              <Link to={"/settings"}>
                <Dropdown.Item>Settings</Dropdown.Item>
              </Link>
            </Dropdown.Item>
            <Dropdown.Item eventKey="2">
              <Link to={"/login"}>
                <Dropdown.Item onClick={logOut}>Log out</Dropdown.Item>
              </Link>
            </Dropdown.Item>
          </Dropdown.Menu>
        </Dropdown>
      </div>
    </nav>
  );
};

export default Topbar;