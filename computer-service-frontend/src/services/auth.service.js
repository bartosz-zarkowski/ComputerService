import axios from 'axios'
import authHeader from './auth-header'

const API_URL = 'https://localhost:7071/api/v1/authentication'

class AuthService {
  login (email, password) {
    return axios
      .post(API_URL, null, {
        params: {
          email,
          password
        }
      })
      .then(response => {
        if (response.data) {
          localStorage.setItem('user', JSON.stringify(response.data))
        }
        return response.data
      })
  }

  logout () {
    if (!this.isTokenExpired())
        axios.post(API_URL + '/cancel', null, { headers: authHeader() })
    localStorage.removeItem('user')
  }

  getCurrentUser () {
    return JSON.parse(localStorage.getItem('user'))
  }

  getToken () {
    var user = this.getCurrentUser()
    if (user)
        return user.data.accessToken
  }

  parseJwt (token) {
    try {
      return JSON.parse(atob(token.split('.')[1]))
    } catch (e) {
      return null
    }
  }

  isTokenExpired () {
    const token = this.getToken()
    if (token) {
      const decodedJwt = this.parseJwt(token)
      if (decodedJwt.exp * 1000 < Date.now()) {
        return true;
      }
      return false;
    }
  }
}

export default new AuthService()
