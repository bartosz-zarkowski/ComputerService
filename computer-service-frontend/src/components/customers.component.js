import React, { Component } from "react";
import { Navigate } from "react-router-dom";
import AuthService from "../services/auth.service";
import CustomerService from "../services/customer.service";


export default class Customers extends Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: undefined,
      isTechnician: false,
      isReceiver: false,
      isAdmin: false,
      customers: ""
    }
  }

  componentDidMount () {
    const user = AuthService.getCurrentUser()
    if (!user) this.setState({ redirect: "/login" })
    else {
      let isTechnician = user.data.userData.role == 'Technician';
      let isReceiver = user.data.userData.role == 'Receiver';
      let isAdmin = user.data.userData.role == 'Administrator';
      let customers = CustomerService.getCustomers();
      this.setState({
        currentUser: user,
        isTechnician: isTechnician,
        isReceiver: isReceiver,
        isAdmin: isAdmin,
        customers: customers
      })
    }
  }

  render() {
    if (this.state.redirect) {
      return <Navigate to={this.state.redirect} />
    }
    const { 
      currentUser, showModeratorBoard, showAdminBoard } = this.state
    return (
      <div>
        Customers
        {localStorage.getItem("customers")}
      </div>
    );
  }
}
