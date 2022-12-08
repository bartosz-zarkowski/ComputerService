import axios from 'axios';
import authHeader from './auth-header';

const API_URL = 'https://localhost:7071/api/v1/';

class CustomerService {
    getCustomers() {
        axios.get(API_URL + 'customers', { headers: authHeader() })
        .then(response => {
            if (response.data)
                localStorage.setItem('customers', JSON.stringify(response.data))
            return response;
        })
    }
}

export default new CustomerService();