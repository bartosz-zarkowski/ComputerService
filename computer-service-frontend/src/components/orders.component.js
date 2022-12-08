import React, { Component } from "react";
import { Navigate } from "react-router-dom";
import AuthService from "../services/auth.service";
import OrderService from "../services/order.service";

export default class Orders extends Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: undefined,
      isTechnician: false,
      isReceiver: false,
      isAdmin: false,
      orders: ""
    }
  }

  componentDidMount () {
    const user = AuthService.getCurrentUser()
    if (!user) this.setState({ redirect: "/login" })
    else {
      let isTechnician = user.data.userData.role == 'Technician'
      let isReceiver = user.data.userData.role == 'Receiver'
      let isAdmin = user.data.userData.role == 'Administrator'
      let orders = OrderService.getOrders();
      this.setState({
        currentUser: user,
        isTechnician: isTechnician,
        isReceiver: isReceiver,
        isAdmin: isAdmin,
        orders: orders
      })
    }
  }

  render() {
    if (this.state.redirect) {
      return <Navigate to={this.state.redirect} />
    }
    const { 
      currentUser, showModeratorBoard, showAdminBoard, orders} = this.state
    return (
      <div>
        Show Orders
        {localStorage.getItem("orders")}
      </div>
    );
  }
}
