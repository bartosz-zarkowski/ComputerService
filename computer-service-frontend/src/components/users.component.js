import React, { Component } from "react";
import AuthService from "../services/auth.service";
import { Navigate } from "react-router-dom";
import UserService from "../services/user.service";

export default class Users extends Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: undefined,
      isTechnician: false,
      isReceiver: false,
      isAdmin: false,
      users: ""
    }
  }

  componentDidMount () {
    const user = AuthService.getCurrentUser()
    if (!user) this.setState({ redirect: "/login" })
    else {
      let isTechnician = user.data.userData.role == 'Technician'
      let isReceiver = user.data.userData.role == 'Receiver'
      let isAdmin = user.data.userData.role == 'Administrator'
      let users = UserService.getUsers();
      this.setState({
        currentUser: user,
        isTechnician: isTechnician,
        isReceiver: isReceiver,
        isAdmin: isAdmin,
        users: users
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
        Users
        {localStorage.getItem("users")}
      </div>
    );
  }
}
