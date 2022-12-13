import axios from "axios";
import authHeader from "../auth/auth-header";

const API_URL = process.env.REACT_APP_API_URL + "customers";

class CustomerService {
  getCustomers() {
    axios.get(API_URL, { headers: authHeader() }).then((response) => {
      if (response.data)
        sessionStorage.setItem("customers", JSON.stringify(response.data));
      return response;
    });
  }
}

export default new CustomerService();