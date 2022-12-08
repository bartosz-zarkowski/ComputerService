import React, { Component } from "react";
import AuthService from "../services/auth.service";
import { Navigate } from "react-router-dom";

export default class Settings extends Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: undefined,
      isTechnician: false,
      isReceiver: false,
      isAdmin: false,
    }
  }

  componentDidMount () {
    const user = AuthService.getCurrentUser()
    if (user) {
      let isTechnician = user.data.userData.role == 'Technician'
      let isReceiver = user.data.userData.role == 'Receiver'
      let isAdmin = user.data.userData.role == 'Administrator'
      this.setState({
        currentUser: user,
        isTechnician: isTechnician,
        isReceiver: isReceiver,
        isAdmin: isAdmin,
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
        Settings
      </div>
    );
  }
}
