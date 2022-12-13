import axios from "axios";
import AuthHeader from "../auth/auth-header";

const API_URL = process.env.REACT_APP_API_URL + "userTrackings";

class UserLogsService {
  getUserLogs() {
    axios.get(API_URL, { headers: AuthHeader() }).then((response) => {
      if (response.data)
        sessionStorage.setItem("userLogs", JSON.stringify(response.data));
      return response.data;
    });
  }

  getUserLogsArray() {
    var userLogs = sessionStorage.getItem("userLogs");
    if (userLogs) return JSON.parse(userLogs);
  }
}

export default new UserLogsService();
