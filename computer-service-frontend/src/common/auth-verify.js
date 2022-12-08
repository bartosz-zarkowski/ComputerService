import React, { useEffect } from 'react'
import { withRouter } from './with-router'
import AuthService from '../services/auth.service'

const AuthVerify = props => {
  let location = props.router.location
  useEffect(() => {
    if (AuthService.isTokenExpired()) {
      {
        props.logOut()
      }
    }
  }, [location])

  return <div></div>
}

export default withRouter(AuthVerify)
