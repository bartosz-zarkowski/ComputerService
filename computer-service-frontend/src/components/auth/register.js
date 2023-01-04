import React, { useState, useRef } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Navigate, useNavigate } from "react-router-dom";

import "../../style/register.css";

import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import { isEmail } from "validator";

import { register } from "../../actions/auth";
import PasswordStrengthBar from "react-password-strength-bar";
import RolesService from "../../services/auth/roles";
import { Button, Card, Col, Row } from "react-bootstrap";
import UserService from "../../services/user/user.service";

const required = (value) => {
  if (!value) {
    return (
      <div className="alert alert-danger" role="alert">
        This field is required!
      </div>
    );
  }
};

const validEmail = (value) => {
  if (!isEmail(value)) {
    return (
      <div className="alert alert-danger" role="alert">
        This is not a valid email.
      </div>
    );
  }
};

const vFirstName = (value) => {
  if (value.length < 3 || value.length > 20) {
    return (
      <div className="alert alert-danger" role="alert">
        The firstname must be between 3 and 20 characters.
      </div>
    );
  }
};

const vLastName = (value) => {
  if (value.length < 3 || value.length > 20) {
    return (
      <div className="alert alert-danger" role="alert">
        The lastname must be between 3 and 20 characters.
      </div>
    );
  }
};

const validPassword = (value) => {
  if (value.length < 8) {
    return (
      <div className="alert alert-danger" role="alert">
        'The password must have at least 8 characters.'
      </div>
    );
  }
  if (!value.match(/[A-Z]/)) {
    return (
      <div className="alert alert-danger" role="alert">
        The password must have at least 1 uppercase character.
      </div>
    );
  }
  if (!value.match(/[a-z]/)) {
    return (
      <div className="alert alert-danger" role="alert">
        The password must have at least 1 lower case character.
      </div>
    );
  }
  if (!value.match(/[\d`~!@#$%\^&*()+=|;:'",.<>\/?\\\-]/)) {
    return (
      <div className="alert alert-danger" role="alert">
        The password must have at least 1 symbol.
      </div>
    );
  }
};

const vPhoneNumber = (value) => {
  if (value.length < 9 || value.length > 20) {
    return (
      <div className="alert alert-danger" role="alert">
        The phone number must have at least 9 characters.
      </div>
    );
  }
};

const validRole = (value) => {
  if (value !== "Technician") {
    return (
      <div className="alert alert-danger" role="alert">
        The role must be: Technician, Receiver, Administrator.
      </div>
    );
  }
};

const Register = () => {
  const { user: currentUser } = useSelector((state) => state.auth);
  
  const navigate = useNavigate();
  const form = useRef();
  const checkBtn = useRef();

  const [storedRegisteredUser, SetStoredRegisteredUser] = useState(UserService.GetStoredRegisteredUser());

  const [firstName, setFirstName] = useState(storedRegisteredUser.firstName);
  const [lastName, setLastName] = useState(storedRegisteredUser.lastName);
  const [email, setEmail] = useState(storedRegisteredUser.email);
  const [password, setPassword] = useState("");
  const [phoneNumber, setPhoneNumber] = useState(storedRegisteredUser.phoneNumber);
  const [role, setRole] = useState(storedRegisteredUser.role);
  const [successful, setSuccessful] = useState("");

  const ClearRegisteredUser = () => {
    SetStoredRegisteredUser(UserService.GetStoredRegisteredUser());
    setFirstName(storedRegisteredUser.firstName);
    setLastName(storedRegisteredUser.lastName);
    setEmail(storedRegisteredUser.email);
    setPhoneNumber(storedRegisteredUser.phoneNumber);
    setRole(storedRegisteredUser.role);
  }

  const clearForm = () => {
    UserService.RemoveStoredRegisteredUserAsync();
    ClearRegisteredUser();
    window.location.reload();
  }

  const { message } = useSelector((state) => state.message);
  const dispatch = useDispatch();

  const onChangeFirstName = (e) => {
    const firstName = e.target.value;
    setFirstName(firstName);
    UserService.SetRegisteredUser(
      firstName,
      lastName,
      email,
      phoneNumber,
      role
    );
  };

  const onChangeLastName = (e) => {
    const lastName = e.target.value;
    setLastName(lastName);
    UserService.SetRegisteredUser(
      firstName,
      lastName,
      email,
      phoneNumber,
      role
    );
  };

  const onChangeEmail = (e) => {
    const email = e.target.value;
    setEmail(email);
    UserService.SetRegisteredUser(
      firstName,
      lastName,
      email,
      phoneNumber,
      role
    );
  };

  const onChangePassword = (e) => {
    const password = e.target.value;
    setPassword(password);
  };

  const onChangePhoneNumber = (e) => {
    const phoneNumber = e.target.value;
    setPhoneNumber(phoneNumber);
    UserService.SetRegisteredUser(
      firstName,
      lastName,
      email,
      phoneNumber,
      role
    );
  };

  const onChangeRole = (e) => {
    const role = e.target.value;
    setRole(role);
    UserService.SetRegisteredUser(
      firstName,
      lastName,
      email,
      phoneNumber,
      role
    );
  };

  const onClickClearForm = () => {
    UserService.RemoveStoredRegisteredUserAsync();
    clearForm();
  };

  const handleRegister = (e) => {
    e.preventDefault();

    setSuccessful(false);

    form.current.validateAll();

    if (checkBtn.current.context._errors.length === 0) {
      dispatch(
        register(firstName, lastName, email, password, phoneNumber, role)
      )
        .then(() => {
          setSuccessful(true);
          UserService.RemoveStoredRegisteredUserAsync();
          var registeredUserId = sessionStorage.getItem("RegisteredUserId");
          var userPage = `/users/${registeredUserId}`;
          navigate(userPage);
        })
        .catch(() => {
          setSuccessful(false);
        });
    }
  };

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  if (!RolesService.isAdmin()) {
    return <Navigate to="/home" />;
  }

  return (
    <div className="container-fluid bd-content mt-5">
      <h2 className="content-header">Register User</h2>
      <div className="register-form">
      <Form onSubmit={handleRegister} ref={form}>
        <Card>
          <img
            src="//ssl.gstatic.com/accounts/ui/avatar_2x.png"
            alt="profile-img"
            className="profile-img-card"
          />

            {!successful && (
              <div>
                <div className="form-group">
                  <label htmlFor="firstName">First Name</label>
                  <Input
                    type="text"
                    className="form-control"
                    name="firstName"
                    value={firstName}
                    onChange={onChangeFirstName}
                    validations={[required, vFirstName]}
                  />
                </div>

                <div className="form-group">
                  <label htmlFor="lastName">Last Name</label>
                  <Input
                    type="text"
                    className="form-control"
                    name="lastName"
                    value={lastName}
                    onChange={onChangeLastName}
                    validations={[required, vLastName]}
                  />
                </div>

                <div className="form-group">
                  <label htmlFor="email">Email</label>
                  <Input
                    type="text"
                    className="form-control"
                    name="email"
                    value={email}
                    onChange={onChangeEmail}
                    validations={[required, validEmail]}
                  />
                </div>

                <div className="form-group">
                  <label htmlFor="phoneNumber">Phone Number</label>
                  <Input
                    type="number"
                    className="form-control"
                    name="phoneNumber"
                    value={phoneNumber}
                    onChange={onChangePhoneNumber}
                    validations={[required, vPhoneNumber]}
                  />
                </div>

                <div className="form-group">
                  <label htmlFor="password">Password</label>
                  <Input
                    type="password"
                    className="form-control"
                    name="password"
                    value={password}
                    onChange={onChangePassword}
                    validations={[required, validPassword]}
                  />
                  <PasswordStrengthBar password={password} />
                </div>

                <div className="form-group">
                  <label>Role</label>
                  <select
                    className="btn"
                    value={role}
                    onChange={onChangeRole}
                    validations={[required, validRole]}
                  >
                    <option value="Technician">Technician</option>
                    <option value="Receiver">Receiver</option>
                    <option value="Administrator">Administrator</option>
                  </select>
                </div>
              </div>
            )}

            {(successful === false) && (
              <div className="form-group">
                <div
                  className={
                    successful ? "alert alert-success" : "alert alert-danger"
                  }
                  role="alert"
                >
                  {message}
                </div>
              </div>
            )}
            <Row>
              <Col>
              <CheckButton style={{ display: "none" }} ref={checkBtn} />
              </Col>
            </Row>
        </Card>
        <div className="form-group">
                <Row>
                  <Col>
                  <Button className="danger" variant="danger" onClick={onClickClearForm}>
                    Clear Form
                  </Button>{" "}
                  </Col>
                  <Col>
                  <button className="btn btn-primary btn-block">
                    Sign Up User
                  </button>
                  </Col>
                </Row>
                </div>
        </Form>
      </div>
    </div>
  );
};

export default Register;
