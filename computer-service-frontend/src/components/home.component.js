import React, { Component } from "react";
import { Navigate } from "react-router-dom";
import AuthService from "../services/auth.service";


export default class Home extends Component {
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
    if (!user) this.setState({ redirect: "/login" })
    else {
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

    const { currentUser, isTechnician, isReceiver,  isAdmin } = this.state;

    return (
      <div>
        Home
        {localStorage.getItem("user")}
      </div>
    );
  }
}
