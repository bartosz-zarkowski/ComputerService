import { useEffect } from "react";
import { useLocation } from "react-router-dom";
import AuthService from "./auth.service";

const getToken = () => {
  var user = AuthService.getCurrentUser();
  if (user) return user.data.accessToken;
};

const AuthVerify = (props) => {
  let location = useLocation();

  useEffect(() => {
    const user = JSON.parse(sessionStorage.getItem("user"));

    if (user) {
      const decodedJwt = getToken();
      if (decodedJwt.exp * 1000 < Date.now()) {
        props.logOut();
      }
    }
  }, [location, props]);

  return;
};

export default AuthVerify;
