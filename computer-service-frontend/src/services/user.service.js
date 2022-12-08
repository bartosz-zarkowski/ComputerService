import axios from 'axios';
import authHeader from './auth-header';

const API_URL = 'https://localhost:7071/api/v1/';

class UserService {
    getUserLogs() {
        axios.get(API_URL + 'userTrackings', { headers: authHeader() })
        .then(response => {
            if (response.data)
                localStorage.setItem('userLogs', JSON.stringify(response.data))
            return response;
        })
    }

    getUsers() {
        axios.get(API_URL + 'users', { headers: authHeader() })
        .then(response => {
            if (response.data)
                localStorage.setItem('users', JSON.stringify(response.data))
            return response;
        })
    }
}

export default new UserService();