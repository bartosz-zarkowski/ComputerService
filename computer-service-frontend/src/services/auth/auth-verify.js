import { useEffect } from "react";
import { useLocation } from "react-router-dom";
import AuthService from "./auth.service";
import { withRouter } from "../../common/with-router";

const getToken = () => {
  var user = AuthService.getCurrentUser();
  if (user) return user.data.accessToken;
};

export const getJWTExpirationTime = () => {
  var token = getToken();
  try {
    var expirationTime = JSON.parse(JSON.parse(atob(token.split(".")[1], 'base64').toString('ascii')).exp * 1000);
    return expirationTime;
  } catch (e) {
    return null;
  }
}

const AuthVerify = (props) => {
  let location = useLocation();
  useEffect(() => {
    const user = JSON.parse(sessionStorage.getItem("user"));

    if (user) {
      const JWTExpirationTime = getJWTExpirationTime();
      if (JWTExpirationTime < Date.now()) {
        props.logOut();
      }
    }
  }, [location, props]);
};

export default withRouter(AuthVerify);
