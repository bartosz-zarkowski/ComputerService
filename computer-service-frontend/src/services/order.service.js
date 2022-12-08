import axios from 'axios';
import authHeader from './auth-header';

const API_URL = 'https://localhost:7071/api/v1/';

class OrderService {
    getOrders() {
        axios.get(API_URL + 'orders', { headers: authHeader() })
        .then(response => {
            if (response.data)
                localStorage.setItem('orders', JSON.stringify(response.data))
            return response;
        })
    }
}

export default new OrderService();