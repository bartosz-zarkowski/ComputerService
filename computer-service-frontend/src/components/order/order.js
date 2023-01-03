import React, { useState, useEffect } from "react";
import { Navigate, useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import { MDBTable, MDBTableBody } from "mdb-react-ui-kit";

import OrderService from "../../services/order/order.service";
import OrderDetailsService from "../../services/order/order-details.service";
import { Button, Col, Row } from "react-bootstrap";

import "../../style/table.css";
import "../../style/order.css";

const Order = () => {
  const { user: currentUser } = useSelector((state) => state.auth);
  const [dataFetched, setDataFetched] = useState(false);
  const orderId = useParams().orderId;
  const [order, setOrder] = useState("");
  const [customer, setCustomer] = useState("");
  const [device, setDevice] = useState("");
  const [accessories, setAccessories] = useState("");
  const [createUser, setCreateUser] = useState("");
  const [serviceUser, setServiceUser] = useState("");
  const [completeUser, setCompleteUser] = useState("");
  const [orderDetails, setOrderDetails] = useState("");
  const [chargesSum, setChargesSum] = useState(0.00);

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
    const accessoriesToString = (orderAccessories) => {
      var accessories = "";
      var first = true;
      orderAccessories.forEach((accessory) => {
        if (first) {
          accessories += accessory.name;
          first = !first;
        } else {
          accessories += `, ${accessory.name}`;
        }
      });
      return accessories;
    };

    async function getData() {
      const data = await OrderService.GetOrderAsync(orderId);
      setOrder(data);
      setCustomer(data.customer);
      setDevice(data.devices[0]);
      setCreateUser(data.createUser);
      setServiceUser(data.serviceUser);
      setCompleteUser(data.completeUser);
      setAccessories(accessoriesToString(data.accessories));
      setDataFetched(true);
    }
    if (!order) {
      getData();
    }
  }, []);
  
  useEffect(() => {
    async function getOrderDetails() {
      const orderDetails = await OrderDetailsService.GetOrderDetailsAsync(
        orderId
      );
      var sum = 0.00;
      sum += orderDetails.hardwareCharges + orderDetails.serviceCharges;
      setOrderDetails(orderDetails);
      setChargesSum(sum);
    }
    if (!orderDetails) {
      getOrderDetails();
    }
    
  }, []);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  if (dataFetched) {
    return (
      <div className="container-fluid bd-content table-content mt-5">
        <h2 className="content-header">Order</h2>
  
        <MDBTable className="table mt-5" hover>
          <MDBTableBody>
            <tr className="table-row">
              <td className="table-element-title">Title</td>
              <td className="table-element-data">{order.title}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Description</td>
              <td className="table-element-data">{order.description}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Customer</td>
              <td className="table-element-data">
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Name:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {customer.firstName} {customer.lastName}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Email:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {customer.email}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Phone Number:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {customer.phoneNumber}
                </Col>
              </Row>
              </td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Status</td>
              <td className="table-element-data">{order.status}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Device</td>
              <td className="table-element-data">
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Name:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {device.name}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Serial Number:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {device.serialNumber}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Device Password:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {device.password}
                </Col>
              </Row>
              <Row>
                <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                  Has Warranty:
                </Col>
                <Col sm={12} md={12} lg={6} className="table-data">
                  {device.hasWarranty && (
                    `yes`
                  )}
                  {!device.hasWarranty && (
                    `no`
                  )}
                </Col>
              </Row>
              </td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Device condition</td>
              <td className="table-element-data">{device.condition}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Accessories</td>
              <td className="table-element-data">{accessories}</td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Created</td>
              <td className="table-element-data">
                <Row>
                    <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                      {order.createdAt && (
                        "User:"
                      )}
                    </Col>
                    <Col sm={12} md={12} lg={6} className="table-data">
                    {createUser.firstName} {createUser.lastName}
                    </Col>
                  </Row>
                  <Row>
                    <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                      {order.createdAt && (
                        "Date:"
                      )}
                    </Col>
                    <Col sm={12} md={12} lg={6} className="table-data">
                    {dateTimeOffSetToString(order.createdAt)}
                    </Col>
                  </Row>
              </td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Serviced</td>
              <td className="table-element-data">
                <Row>
                  <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                    {order.updatedAt && (
                      "User:"
                    )}
                  </Col>
                  <Col sm={12} md={12} lg={6} className="table-data">
                    {order.updatedAt && (
                      serviceUser.firstName + " " + serviceUser.lastName
                    )}
                  </Col>
                </Row>
                <Row>
                  <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                    {order.updatedAt && (
                      "Date:"
                    )}
                  </Col>
                  <Col sm={12} md={12} lg={6} className="table-data">
                    {order.updatedAt && (
                      dateTimeOffSetToString(order.updatedAt)
                    )}
                  </Col>
                </Row>
              </td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Received</td>
              <td className="table-element-data">
                <Row>
                  <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                    {order.completedAt && (
                      "User:"
                    )} 
                  </Col>
                  <Col sm={12} md={12} lg={6} className="table-data">
                    {order.completedAt && (
                      completeUser.firstName + " " + completeUser.lastName
                    )}
                  </Col>
                </Row>
                <Row>
                  <Col sm={12} md={12} lg={6} className="table-data-header user-header">
                    {order.completedAt && (
                      "Date:"
                    )}
                  </Col>
                  <Col sm={12} md={12} lg={6} className="table-data">
                    {order.completedAt && (
                      dateTimeOffSetToString(order.completedAt)
                    )}
                  </Col>
                </Row>
              </td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Service description</td>
              <td className="table-element-data">
                {orderDetails && (
                  orderDetails.serviceDescription
                )}
              </td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Service comment</td>
              <td className="table-element-data">
                {orderDetails && (
                  orderDetails.additionalInformation
                )}
              </td>
            </tr>
            <tr className="table-row">
              <td className="table-element-title">Valuation</td>
              <td className="table-element-data">
                <Row>
                  <Col sm={12} md={6} lg={6} className="table-data-header">
                  Hardware charges:
                  </Col>
                  <Col sm={12} md={6} lg={6} className="table-data">
                    {orderDetails && (
                      orderDetails.hardwareCharges.toFixed(2) + ' zł.'
                    )}
                  </Col>
                </Row>
                <Row>
                  <Col sm={12} md={6} lg={6} className="table-data-header">
                    Service charges:
                  </Col>
                  <Col sm={12} md={6} lg={6} className="table-data">
                    {orderDetails && (
                      orderDetails.serviceCharges.toFixed(2) + ' zł.'
                    )}
                  </Col>
                </Row>
                <Row>
                  <Col sm={12} md={6} lg={6} className="table-data-header">
                  Sum:
                  </Col>
                  <Col sm={12} md={6} lg={6} className="table-data">
                    {orderDetails && (
                      chargesSum.toFixed(2) + ' zł.'
                    )}
                  </Col>
                </Row>
              </td>
            </tr>
          </MDBTableBody>
        </MDBTable>
        <Row className="order-buttons">
          <Col className="delete-order-col">
            <Button className="danger delete-order-btn" variant="danger">
              Delete Order
            </Button>{" "}
          </Col>
          <Col className="edit-order-col">
          <Button className="edit-order-btn">
            Edit Order
          </Button>
          </Col>
        </Row>
      </div>
    );
  } else {
    return (
      <div className="container-fluid bd-content table-content mt-5">
        <h2 className="content-header">Order</h2>
        <div className="loading header mt-5">Loading...</div>
    </div>
    );
  }
};

export default Order;
