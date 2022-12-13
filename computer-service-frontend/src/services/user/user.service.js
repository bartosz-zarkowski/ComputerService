import axios from "axios";
import authHeader from "../auth/auth-header";

const API_URL = process.env.REACT_APP_API_URL + "users";

class UserService {
  getUsers() {
    axios.get(API_URL, { headers: authHeader() }).then((response) => {
      if (response.data)
        sessionStorage.setItem("users", JSON.stringify(response.data));
      return response;
    });
  }
}

export default new UserService();