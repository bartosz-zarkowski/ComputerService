import React, { useState, useRef } from "react";
import { useSelector } from "react-redux";
import { Navigate, useNavigate } from "react-router-dom";

import "../../style/order-form.css";

import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import { isEmail } from "validator";

import RolesService from "../../services/auth/roles";
import {
  Form as BootstrapForm,
  Card,
  FormGroup,
  Col,
  Row,
  Button,
  Modal,
} from "react-bootstrap";
import OrderService from "../../services/order/order.service";
import CustomerService from "../../services/customer/customer.service";
import DeviceService from "../../services/device/device.service";
import { Asterisk, ExclamationDiamondFill } from "react-bootstrap-icons";
import OrderAccessoryService from "../../services/order-accessory/order-accessory.service";
import DataTable from "react-data-table-component";
import AddressService from "../../services/address/address-service";

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

const vPhoneNumber = (value) => {
  if (value.length < 9 || value.length > 20) {
    return (
      <div className="alert alert-danger" role="alert">
        Invalid phone number.
      </div>
    );
  }
};

const vTitle = (value) => {
  if (value.length < 5) {
    return (
      <div className="alert alert-danger" role="alert">
        Title is too short.
      </div>
    );
  }
};

const OrderForm = () => {
  const form = useRef();
  const checkBtn = useRef();
  let navigate = useNavigate();

  const [selectedRows, setSelectedRows] = useState(false);
  const [toggledClearRows, setToggleClearRows] = useState(false);

  const handleChange = ({ selectedRows }) => {
    setSelectedRows(selectedRows);
  };

  // CUSTOMER
  const [storedCustomer, setStoredCustomer] = useState(CustomerService.GetStoredCustomer());
  const customerId = useState(storedCustomer.customerId);
  const [customerFirstName, setCustomerFirstName] = useState(
    storedCustomer.firstName
  );
  const [customerLastName, setCustomerLastName] = useState(
    storedCustomer.lastName
  );
  const [customerEmail, setCustomerEmail] = useState(storedCustomer.email);
  const [customerPhoneNumber, setCustomerPhoneNumber] = useState(
    storedCustomer.phoneNumber
  );

  //ADDRESS
  const [storedAddress, setStoredAddress] = useState(AddressService.GetStoredAddress());
  const addressId = useState(storedAddress.addressId);
  const [country, setCountry] = useState(
    storedAddress.country
  );
  const [state, setState ] = useState(
    storedAddress.state 
  );
  const [city, setCity] = useState(
    storedAddress.city
  );
  const [postalCode, setPostalCode] = useState(
    storedAddress.postalCode
  );
  const [street, setStreet ] = useState(
    storedAddress.street
  );
  const [streetNumber, setStreetNumber] = useState(
    storedAddress.streetNumber
  );
  const [apartment, setApartment] = useState(
    storedAddress.apartment
  );

  // ORDER
  const [storedOrder, setStoredOrder] = useState(OrderService.GetStoredOrder());
  const orderId = useState(storedOrder.orderId);
  const [orderTitle, setOrderTitle] = useState(storedOrder.title);
  const [orderDescription, setOrderDescription] = useState(
    storedOrder.description
  );

  // DEVICE
  const [storedOrderDevice, setStoredDevice] = useState(DeviceService.GetStoredOrderDevice());
  const [deviceName, setDeviceName] = useState(storedOrderDevice.name);
  const [deviceSerialNumber, setDeviceSerialNumber] = useState(
    storedOrderDevice.serialNumber
  );
  const [devicePassword, setDevicePassword] = useState(
    storedOrderDevice.password
  );
  const [deviceCondition, setDeviceCondition] = useState(
    storedOrderDevice.condition
  );
  const [deviceHasWarranty, setDeviceHasWarranty] = useState(
    storedOrderDevice.hasWarranty
  );

  // ACCESSORY
  const [accessoryName, setAccessoryName] = useState("");
  const [storedAccessories, setStoredAccessories] = useState(OrderAccessoryService.GetStoredOrderAccessories());
  const [orderAccesories, setOrderAccessories] = useState(storedAccessories);

  const [isChecked, setIsChecked] = useState(deviceHasWarranty);

  const [successful, setSuccessful] = useState(false);

  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  if (RolesService.isTechnician()) {
    return <Navigate to="/home" />;
  }

  const clearCustomer = () => {
    setStoredCustomer(CustomerService.GetStoredCustomer());
    setCustomerFirstName(storedCustomer.firstName);
    setCustomerLastName(storedCustomer.lastName);
    setCustomerEmail(storedCustomer.email);
    setCustomerPhoneNumber(storedCustomer.phoneNumber);
  }

  const clearAddress = () => {
    setStoredAddress(AddressService.GetStoredAddress());
    setCountry(storedAddress.country);
    setState(storedAddress.state );
    setCity(storedAddress.city);
    setPostalCode(storedAddress.postalCode);
    setStreet(storedAddress.street);
    setStreetNumber(storedAddress.streetNumber);
    setApartment(storedAddress.apartment);
  }

  const clearForm = () => {
    OrderService.removeStorageAsync();
    clearCustomer();
    clearAddress();
    setStoredOrder(OrderService.GetStoredOrder());
    setStoredDevice(DeviceService.GetStoredOrderDevice());
    setStoredAccessories(OrderAccessoryService.GetStoredOrderAccessories());
    window.location.reload();
  }

  const onChangeCustomerFirstName = (e) => {
    const customerFirstName = e.target.value;
    setCustomerFirstName(customerFirstName);
    CustomerService.SetStoredCustomer(
      "",
      customerFirstName,
      customerLastName,
      customerEmail,
      customerPhoneNumber
    );
  };

  const onChangeCustomerLastName = (e) => {
    const customerLastName = e.target.value;
    setCustomerLastName(customerLastName);
    CustomerService.SetStoredCustomer(
      "",
      customerFirstName,
      customerLastName,
      customerEmail,
      customerPhoneNumber
    );
  };

  const onChangeCustomerEmail = (e) => {
    const customerEmail = e.target.value;
    setCustomerEmail(customerEmail);
    CustomerService.SetStoredCustomer(
      "",
      customerFirstName,
      customerLastName,
      customerEmail,
      customerPhoneNumber
    );
  };

  const onChangeCustomerPhoneNumber = (e) => {
    const customerPhoneNumber = e.target.value;
    setCustomerPhoneNumber(customerPhoneNumber);
    CustomerService.SetStoredCustomer(
      "",
      customerFirstName,
      customerLastName,
      customerEmail,
      customerPhoneNumber
    );
  };

  const onChangeCountry = (e) => {
    const country = e.target.value;
    setCountry(country);
    AddressService.SetStoredAddress(
      "",
      country,
      state,
      city,
      postalCode,
      street,
      streetNumber,
      apartment,
    );
  };

  const onChangeState  = (e) => {
    const state = e.target.value;
    setState(state);
    AddressService.SetStoredAddress(
      "",
      country,
      state,
      city,
      postalCode,
      street,
      streetNumber,
      apartment,
    );
  };
  
  const onChangePostalCode  = (e) => {
    const postalCode = e.target.value;
    setPostalCode(postalCode);
    AddressService.SetStoredAddress(
      "",
      country,
      state,
      city,
      postalCode,
      street,
      streetNumber,
      apartment,
    );
  };

  const onChangeCity  = (e) => {
    const city = e.target.value;
    setCity(city);
    AddressService.SetStoredAddress(
      "",
      country,
      state,
      city,
      postalCode,
      street,
      streetNumber,
      apartment,
    );
  };

  const onChangeStreetNumber  = (e) => {
    const streetNumber = e.target.value;
    setStreetNumber(streetNumber);
    AddressService.SetStoredAddress(
      "",
      country,
      state,
      city,
      postalCode,
      street,
      streetNumber,
      apartment,
    );
  };

  const onChangeApartment  = (e) => {
    const apartment = e.target.value;
    setApartment(apartment);
    AddressService.SetStoredAddress(
      "",
      country,
      state,
      city,
      postalCode,
      street,
      streetNumber,
      apartment,
    );
  };

  const onChangeStreet  = (e) => {
    const street = e.target.value;
    setStreet(street);
    AddressService.SetStoredAddress(
      "",
      country,
      state,
      city,
      postalCode,
      street,
      streetNumber,
      apartment,
    );
  };

  const onChangeOrderTitle = (e) => {
    const orderTitle = e.target.value;
    setOrderTitle(orderTitle);
    OrderService.SetStoredOrder("", "", orderTitle, orderDescription);
  };

  const onChangeOrderDescription = (e) => {
    const orderDescription = e.target.value;
    setOrderDescription(orderDescription);
    OrderService.SetStoredOrder("", "", orderTitle, orderDescription);
  };

  const OnChangeDeviceName = (e) => {
    const deviceName = e.target.value;
    setDeviceName(deviceName);
    DeviceService.SetStoredOrderDevice(
      "",
      deviceName,
      deviceSerialNumber,
      devicePassword,
      deviceCondition,
      deviceHasWarranty,
      customerId,
      orderId
    );
  };

  const OnChangeDeviceSerialNumber = (e) => {
    const deviceSerialNumber = e.target.value;
    setDeviceSerialNumber(deviceSerialNumber);
    DeviceService.SetStoredOrderDevice(
      "",
      deviceName,
      deviceSerialNumber,
      devicePassword,
      deviceCondition,
      deviceHasWarranty,
      customerId,
      orderId
    );
  };

  const OnChangeDevicePassword = (e) => {
    const devicePassword = e.target.value;
    setDevicePassword(devicePassword);
    DeviceService.SetStoredOrderDevice(
      "",
      deviceName,
      deviceSerialNumber,
      devicePassword,
      deviceCondition,
      deviceHasWarranty,
      customerId,
      orderId
    );
  };

  const onChangeDeviceCondition = (e) => {
    const deviceCondition = e.target.value;
    setDeviceCondition(deviceCondition);
    DeviceService.SetStoredOrderDevice(
      "",
      deviceName,
      deviceSerialNumber,
      devicePassword,
      deviceCondition,
      deviceHasWarranty,
      customerId,
      orderId
    );
  };

  const onChangeDeviceHasWarranty = (e) => {
    setIsChecked(!isChecked);
    setDeviceHasWarranty(!isChecked);
    DeviceService.SetStoredOrderDevice(
      "",
      deviceName,
      deviceSerialNumber,
      devicePassword,
      deviceCondition,
      !deviceHasWarranty,
      customerId,
      orderId
    );
  };

  const onChangeAccessoryName = (e) => {
    const accessoryName = e.target.value;
    setAccessoryName(accessoryName);
  };

  const onClickAddAccessory = () => {
    if (accessoryName.length > 1) {
      OrderAccessoryService.SetStoredOrderAccessories(accessoryName);
      setOrderAccessories(OrderAccessoryService.GetStoredOrderAccessories());
      setAccessoryName("");
    }
  };

  const onClickClearForm = () => {
    OrderService.removeStorageAsync();
    clearForm();
  };

  const handleCreateOrder = (e) => {
    e.preventDefault();

    setSuccessful(false);

    form.current.validateAll();

    if (checkBtn.current.context._errors.length === 0) {
      var order = OrderService.CreateOrderAsync();
      order
        .then(() => {
          setSuccessful(true);
          // OrderService.removeStorageAsync();
          // window.location.reload();
        })
        .catch(() => {
          setSuccessful(false);
        });
    }
  };

  return (
    <div className="order-form">
      <Form onSubmit={handleCreateOrder} ref={form}>
        <Card className="text-center">
          <Card.Body>
            <Card.Title>
              <h3>Order</h3>
            </Card.Title>
            <FormGroup className="form-control-order">
              <label htmlFor="orderTitle">
                Title{" "}
                <sup>
                  <Asterisk size={9} />
                </sup>
              </label>
              <Input
                type="text"
                className="form-control"
                name="orderTitle"
                value={orderTitle}
                onChange={onChangeOrderTitle}
                validations={[required, vTitle]}
              />
            </FormGroup>

            <FormGroup className="form-control-order form-control-description">
              <label htmlFor="orderDescription">Description</label>
              <BootstrapForm.Control
                as="textarea"
                rows={3}
                name="orderDescription"
                value={orderDescription}
                onChange={onChangeOrderDescription}
              />
            </FormGroup>
          </Card.Body>
        </Card>

        <Card className="text-center">
          <Card.Body>
            <Card.Title>
              <h3>Customer</h3>
            </Card.Title>
            <Row>
              <Col className="pt-3">
                <Card.Title className="pt-3">Informations</Card.Title>
                <FormGroup className="form-control-order">
                  <label htmlFor="customerFirstName">
                    First Name{" "}
                    <sup>
                      <Asterisk size={9} />
                    </sup>
                  </label>
                  <Input
                    type="text"
                    className="form-control"
                    name="customerFirstName"
                    value={customerFirstName}
                    onChange={onChangeCustomerFirstName}
                    validations={[required, vFirstName]}
                  />
                </FormGroup>

                <FormGroup className="form-control-order">
                  <label htmlFor="customerLastName">
                    Last Name{" "}
                    <sup>
                      <Asterisk size={9} />
                    </sup>
                  </label>
                  <Input
                    type="text"
                    className="form-control"
                    name="customerLastName"
                    value={customerLastName}
                    onChange={onChangeCustomerLastName}
                    validations={[required, vLastName]}
                  />
                </FormGroup>

                <FormGroup className="form-control-order">
                  <label htmlFor="customerEmail">
                    Email{" "}
                    <sup>
                      <Asterisk size={9} />
                    </sup>
                  </label>
                  <Input
                    type="text"
                    className="form-control"
                    name="customerEmail"
                    value={customerEmail}
                    onChange={onChangeCustomerEmail}
                    validations={[required, validEmail]}
                  />
                </FormGroup>

                <FormGroup className="form-control-order">
                  <label htmlFor="customerPhoneNumber">
                    PhoneNumber{" "}
                    <sup>
                      <Asterisk size={9} />
                    </sup>
                  </label>
                  <Input
                    type="number"
                    className="form-control"
                    name="customerPhoneNumber"
                    value={customerPhoneNumber}
                    onChange={onChangeCustomerPhoneNumber}
                    validations={[required, vPhoneNumber]}
                  />
                </FormGroup>
              </Col>
            </Row>

            <Row>
              <Col className="pt-3">
                <Card.Title className="pt-3">Address</Card.Title>

                <Row>
                  <Col>
                    <FormGroup className="form-control-order">
                      <label htmlFor="country">
                        Country{" "}
                        <sup>
                          <Asterisk size={9} />
                        </sup>
                      </label>
                      <Input
                        type="text"
                        className="form-control"
                        name="country"
                        value={country}
                        onChange={onChangeCountry}
                        validations={[required]}
                      />
                    </FormGroup>
                  </Col>
                  <Col>
                    <FormGroup className="form-control-order">
                      <label htmlFor="state">
                        State{" "}
                        <sup>
                          <Asterisk size={9} />
                        </sup>
                      </label>
                      <Input
                        type="text"
                        className="form-control"
                        name="state"
                        value={state}
                        onChange={onChangeState}
                        validations={[required]}
                      />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col>
                    <FormGroup className="form-control-order">
                      <label htmlFor="postalCode">
                        Postal Code{" "}
                        <sup>
                          <Asterisk size={9} />
                        </sup>
                      </label>
                      <Input
                        type="text"
                        className="form-control"
                        name="postalCode"
                        value={postalCode}
                        onChange={onChangePostalCode}
                        validations={[required]}
                      />
                    </FormGroup>
                  </Col>
                  <Col sm={8}>
                    <FormGroup className="form-control-order">
                      <label htmlFor="city">
                        City{" "}
                        <sup>
                          <Asterisk size={9} />
                        </sup>
                      </label>
                      <Input
                        type="text"
                        className="form-control"
                        name="city"
                        value={city}
                        onChange={onChangeCity}
                        validations={[required]}
                      />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col sm={3}>
                  <FormGroup className="form-control-order">
                      <label htmlFor="streetNumber">
                        S. Number{" "}
                        <sup>
                          <Asterisk size={9} />
                        </sup>
                      </label>
                      <Input
                        type="text"
                        className="form-control"
                        name="streetNumber"
                        value={streetNumber}
                        onChange={onChangeStreetNumber}
                        validations={[required]}
                      />
                    </FormGroup>
                  </Col>
                  <Col sm={3}>
                  <FormGroup className="form-control-order">
                      <label htmlFor="apartment">
                        apartment{" "}
                      </label>
                      <Input
                        type="text"
                        className="form-control"
                        name="apartment"
                        value={apartment}
                        onChange={onChangeApartment}
                        validations={[]}
                      />
                    </FormGroup>
                  </Col>
                  <Col sm={6}>
                  <FormGroup className="form-control-order">
                      <label htmlFor="street">
                        Street{" "}
                        <sup>
                          <Asterisk size={9} />
                        </sup>
                      </label>
                      <Input
                        type="text"
                        className="form-control"
                        name="street"
                        value={street}
                        onChange={onChangeStreet}
                        validations={[required]}
                      />
                    </FormGroup>
                  </Col>
                </Row>
              </Col>
            </Row>
          </Card.Body>
        </Card>

        <Card className="text-center">
          <Card.Body>
            <Card.Title>
              <h3>Details</h3>
            </Card.Title>
            <Row>
              <Col>
                <Card.Title className="pt-3">Device</Card.Title>
                <FormGroup className="form-control-order">
                  <label htmlFor="deviceName">
                    Name{" "}
                    <sup>
                      <Asterisk size={9} />
                    </sup>
                  </label>
                  <Input
                    type="text"
                    className="form-control"
                    name="deviceName"
                    value={deviceName}
                    onChange={OnChangeDeviceName}
                    validations={[required]}
                  />
                </FormGroup>

                <FormGroup className="form-control-order">
                  <label htmlFor="deviceSerialNumber">Serial Number</label>
                  <Input
                    type="text"
                    className="form-control"
                    name="deviceSerialNumber"
                    value={deviceSerialNumber}
                    onChange={OnChangeDeviceSerialNumber}
                  />
                </FormGroup>

                <FormGroup className="form-control-order">
                  <label htmlFor="devicePassword">
                    Device Password <ExclamationDiamondFill size={18} />
                  </label>
                  <Input
                    type="text"
                    className="form-control"
                    name="devicePassword"
                    value={devicePassword}
                    onChange={OnChangeDevicePassword}
                  />
                </FormGroup>

                <FormGroup className="form-control-order form-control-description">
                  <label htmlFor="deviceCondition">Device Condition</label>
                  <BootstrapForm.Control
                    as="textarea"
                    rows={1}
                    name="deviceCondition"
                    value={deviceCondition}
                    onChange={onChangeDeviceCondition}
                  />
                </FormGroup>

                <FormGroup className="mt-3">
                  <BootstrapForm.Check
                    type="switch"
                    name="deviceHasWarranty"
                    label="Has warranty"
                    id="custom-switch"
                    checked={deviceHasWarranty}
                    onChange={onChangeDeviceHasWarranty}
                  />
                </FormGroup>
              </Col>
            </Row>

            <Row>
              <Col className="pt-3">
                <Card.Title>Accessories</Card.Title>
                <Row>
                  <Col sm={8}>
                    <label htmlFor="accessoryName">Accessory Name</label>
                    <FormGroup className="form-control-order form-control-description">
                      <Input
                        type="text"
                        className="form-control"
                        label="Accessory Name"
                        name="accessoryName"
                        value={accessoryName}
                        onChange={onChangeAccessoryName}
                      />
                    </FormGroup>
                  </Col>
                  <Col className="add-accessory-button" sm={4}>
                    <Button className="btn-block" onClick={onClickAddAccessory}>
                      Add Accessory
                    </Button>
                  </Col>
                </Row>
                <Row>
                  <Col>
                    {orderAccesories.length > 0 && (
                      <DataTable
                        className="order-accessories-table mt-3"
                        columns={[
                          {
                            name: "Accessory Name",
                            selector: (row) => `${row.name}`,
                          },
                        ]}
                        data={orderAccesories}
                        noTableHead
                      />
                    )}
                  </Col>
                </Row>
              </Col>
            </Row>
          </Card.Body>
        </Card>
        <Row>
          <Col>
            <Button className="danger" variant="danger" onClick={onClickClearForm}>
              Clear Form
            </Button>{" "}
          </Col>
          <Col>
            <div className="form-group">
              <button className="btn btn-primary btn-block">
                Create Order
              </button>
            </div>
            <CheckButton style={{ display: "none" }} ref={checkBtn} />
          </Col>
        </Row>
      </Form>
    </div>
  );
};

export default OrderForm;
