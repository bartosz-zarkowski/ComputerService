import React, { Component } from "react";
import { Navigate } from "react-router-dom";
import AuthService from "../services/auth.service";
import UserService from "../services/user.service";

export default class UserLogs extends Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: undefined,
      isTechnician: false,
      isReceiver: false,
      isAdmin: false,
      userLogs: ""
    }
  }

  componentDidMount () {
    const user = AuthService.getCurrentUser()
    if (!user) this.setState({ redirect: "/login" })
    else {
      let isTechnician = user.data.userData.role == 'Technician'
      let isReceiver = user.data.userData.role == 'Receiver'
      let isAdmin = user.data.userData.role == 'Administrator'
      let userLogs = UserService.getUserLogs();
      this.setState({
        currentUser: user,
        isTechnician: isTechnician,
        isReceiver: isReceiver,
        isAdmin: isAdmin,
        userLogs: userLogs
      })
    }
  }

  render() {
    if (this.state.redirect) {
      return <Navigate to={this.state.redirect} />
    }
    const currentUser = this.state
    return (
      <div>
        User logs
        {localStorage.getItem("userLogs")}
      </div>
    );
  }
}
