import React, { useState, useEffect } from "react";
import { Navigate, useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import { MDBTable, MDBTableBody } from "mdb-react-ui-kit";
import { Button, Col, Row } from "react-bootstrap";

import CustomerService from "../../services/customer/customer.service";
import AddressService from "../../services/address/address-service";

import "../../style/table.css";
import "../../style/customer.css";
import RolesService from "../../services/auth/roles";

const Customer = () => {
  const { user: currentUser } = useSelector((state) => state.auth);
  const [dataFetched, setDataFetched] = useState(false);
  const customerId = useParams().customerId;
  const [customer, setCustomer] = useState("");
  const [address, setAddress] = useState("");

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
      const customer = await CustomerService.GetCustomerAsync(customerId);
      const address = await AddressService.GetCustomerAddressAsync(customerId);
      setCustomer(customer);
      setAddress(address);
      setDataFetched(true);
    }
    if (!customer) {
      getData();
    }
  }, []);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  if (RolesService.isTechnician()) {
    return <Navigate to="/home" />;
  }
  
  if (dataFetched) {
    return (
      <div className="container-fluid bd-content customer-content table-content mt-5">
        <h2 className="content-header">Customer</h2>
        <MDBTable className="table mt-5" hover>
          <MDBTableBody>
            <tr className="table-row">
              <td className="table-element-title">First Name</td>
              <td className="table-element-data">{customer.firstName}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Last Name</td>
              <td className="table-element-data">{customer.lastName}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Email</td>
              <td className="table-element-data">{customer.email}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Phone Number</td>
              <td className="table-element-data">{customer.phoneNumber}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Address</td>
              <td className="table-element-data">
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Country:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {address && (
                    address.country
                  )}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  State:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {address && (
                    address.state
                  )}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  City:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {address && (
                    address.city
                  )}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Postal Code:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {address && (
                    address.postalCode
                  )}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Street:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {address && (
                    address.street
                  )}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Street Number:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {address && (
                    address.streetNumber
                  )}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Appartment:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {address && (
                    address.appartment
                  )}
                </Col>
              </Row>
              </td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Created At</td>
              <td className="table-element-data">{dateTimeOffSetToString(customer.createdAt)}</td>
            </tr>
          </MDBTableBody>
        </MDBTable>
        <Row className="customer-buttons">
          <Col className="delete-customer-col">
            <Button className="danger delete-customer-btn" variant="danger">
              Delete Customer
            </Button>{" "}
          </Col>
          <Col className="edit-customer-col">
          <Button className="edit-customer-btn">
            Edit Customer
          </Button>
          </Col>
        </Row>
      </div>
    );
  } else {
    return (
      <div className="container-fluid bd-content table-content mt-5">
        <h2 className="content-header">Customer</h2>
        <div className="loading header mt-5">Loading...</div>
    </div>
    );
  }
};

export default Customer;
