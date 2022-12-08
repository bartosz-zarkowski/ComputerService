import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import './App.css'

import React, { Component } from 'react'
import { Routes, Route, Link } from 'react-router-dom'
import {
  House,
  PlusCircleFill,
  List,
  PencilSquare,
  PersonSquare,
  PeopleFill,
  PersonCircle
} from 'react-bootstrap-icons'
import { Dropdown } from 'react-bootstrap'
import AuthService from './services/auth.service'
import AuthVerify from './common/auth-verify'

import Login from './components/login.component'
import Home from './components/home.component'
import CreateOrder from './components/create-order.component'
import Orders from './components/orders.component'
import UserLogs from './components/user-logs.component'
import Users from './components/users.component'
import Customers from './components/customers.component'
import Settings from './components/settings.component'

window.addEventListener('DOMContentLoaded', event => {
  const sidebarToggle = document.body.querySelector('#sidebarToggle')
  if (sidebarToggle) {
    sidebarToggle.addEventListener('click', event => {
      event.preventDefault()
      document.body.classList.toggle('sb-sidenav-toggled')
      localStorage.setItem(
        'sb|sidebar-toggle',
        document.body.classList.contains('sb-sidenav-toggled')
      )
    })
  }
})

const user = AuthService.getCurrentUser()

const CustomToggle = React.forwardRef(({ children, onClick }, ref) => (
  <div
    ref={ref}
    onClick={e => {
      e.preventDefault()
      onClick(e)
    }}
  >
    <div className='user row rounded'>
      <div className='col-6'>
        {user.data.userData.firstName + ' ' + user.data.userData.firstName}
      </div>
      <div className='col-6'>
        <PersonCircle size={50} />
      </div>
    </div>
  </div>
))

class App extends Component {
  constructor (props) {
    super(props)
    this.logOut = this.logOut.bind(this)

    this.state = {
      currentUser: undefined,
      isTechnician: false,
      isReceiver: false,
      isAdmin: false
    }
  }

  componentDidMount () {
    const user = AuthService.getCurrentUser()
    if (user) {
      var isTechnician = user.data.userData.role == 'Technician'
      var isReceiver = user.data.userData.role == 'Receiver'
      var isAdmin = user.data.userData.role == 'Administrator'
      this.setState({
        currentUser: user,
        isTechnician: isTechnician,
        isReceiver: isReceiver,
        isAdmin: isAdmin
      })
    }
  }

  logOut () {
    AuthService.logout()
    this.setState({
      currentUser: undefined,
      isTechnician: false,
      isReceiver: false,
      isAdmin: false
    })
  }

  renderCreateOrderLink = () => {
    ;<Link
      to={'/create-order'}
      className='list-group-item list-group-item-action p-3 rounded'
    >
      <div className='nav-item row'>
        <div className='col-3'>
          <PlusCircleFill size={30} />
        </div>
        <div className='col-9 mt-1'>Dodaj zlecenie</div>
      </div>
    </Link>
  }

  render () {
    const { currentUser, isTechnician, isReceiver, isAdmin } = this.state

    return (
      <div className='whole-content container-fluid px-0'>
        {currentUser ? (
          <div className='sensitive-content container-fluid px-0 mx-0'>
            <nav className='topbar navbar navbar-expand navbar-dark'>
              <Link to={'/login'} className='navbar-brand'>
                <img src={require('./Logo.png')} />
              </Link>
              <div className='userDropdown'>
                <Dropdown>
                  <Dropdown.Toggle
                    as={CustomToggle}
                    id='dropdown-custom-components'
                  ></Dropdown.Toggle>

                  <Dropdown.Menu>
                    <Dropdown.Item eventKey='1'>
                      <Link to={'/settings'}>
                        <Dropdown.Item>Ustawienia</Dropdown.Item>
                      </Link>
                    </Dropdown.Item>
                    <Dropdown.Item eventKey='2'>
                      <Link to={'/login'}>
                        <Dropdown.Item onClick={this.logOut}>
                          Wyloguj
                        </Dropdown.Item>
                      </Link>
                    </Dropdown.Item>
                  </Dropdown.Menu>
                </Dropdown>
              </div>
            </nav>

            <div className='wrapper d-flex' id='wrapper'>
              <div className='sidebar border-end' id='sidebar-wrapper'>
                <div className='list-group list-group-flush px-3 pt-3 pb-3'>
                  <Link
                    to={'/home'}
                    className='list-group-item list-group-item-action p-3 rounded'
                  >
                    <div className='nav-item row'>
                      <div className='col-3'>
                        <House size={30} />
                      </div>
                      <div className='col-9 mt-1'>Strona główna</div>
                    </div>
                  </Link>

                  {isAdmin && (
                    <Link
                      to={'/create-order'}
                      className='list-group-item list-group-item-action p-3 rounded'
                    >
                      <div className='nav-item row'>
                        <div className='col-3'>
                          <PlusCircleFill size={30} />
                        </div>
                        <div className='col-9 mt-1'>Dodaj zlecenie</div>
                      </div>
                    </Link>
                  )}
                
                {isReceiver && (
                    <Link
                      to={'/create-order'}
                      className='list-group-item list-group-item-action p-3 rounded'
                    >
                      <div className='nav-item row'>
                        <div className='col-3'>
                          <PlusCircleFill size={30} />
                        </div>
                        <div className='col-9 mt-1'>Dodaj zlecenie</div>
                      </div>
                    </Link>
                  )}

                  <Link
                    to={'/orders'}
                    className='list-group-item list-group-item-action p-3 rounded'
                  >
                    <div className='nav-item row'>
                      <div className='col-3'>
                        <List size={30} />
                      </div>
                      <div className='col-9 mt-1'>Wyświetl zlecenia</div>
                    </div>
                  </Link>
                  {isAdmin && (
                    <Link
                      to={'/user-logs'}
                      className='list-group-item list-group-item-action p-3 rounded'
                    >
                      <div className='nav-item row'>
                        <div className='col-3'>
                          <PencilSquare size={30} />
                        </div>
                        <div className='col-9 mt-1'>Logi użytkowników</div>
                      </div>
                    </Link>
                  )}
                  {isAdmin && (
                    <Link
                      to={'/users'}
                      className='list-group-item list-group-item-action p-3 rounded'
                    >
                      <div className='nav-item row'>
                        <div className='col-3'>
                          <PersonSquare size={30} />
                        </div>
                        <div className='col-9 mt-1'>Użytkownicy</div>
                      </div>
                    </Link>
                  )}
                  <Link
                    to={'/customers'}
                    className='list-group-item list-group-item-action p-3 rounded'
                  >
                    <div className='row'>
                      <div className='col-3'>
                        <PeopleFill size={30} />
                      </div>
                      <div className='col-9 mt-1'>Klienci</div>
                    </div>
                  </Link>
                </div>
              </div>
              <div id='page-content wrapper'>
                <div className='container-fluid'>
                  <Routes>
                    <Route path='/' element={<Home />} />
                    <Route path='/home' element={<Home />} />
                    <Route path='/create-order' element={<CreateOrder />} />
                    <Route path='/orders' element={<Orders />} />
                    <Route path='/user-logs' element={<UserLogs />} />
                    <Route path='/users' element={<Users />} />
                    <Route path='/customers' element={<Customers />} />
                    <Route path='/login' element={<Login />} />
                    <Route path='/settings' element={<Settings />} />
                  </Routes>
                </div>
              </div>
            </div>
            <AuthVerify logOut={this.logOut} />
          </div>
        ) : (
          <div className='container-fluid'>
            <Routes>
              <Route path='/' element={<Login />} />
              <Route path='/home' element={<Home />} />
              <Route path='/create-order' element={<CreateOrder />} />
              <Route path='/orders' element={<Orders />} />
              <Route path='/user-logs' element={<UserLogs />} />
              <Route path='/users' element={<Users />} />
              <Route path='/customers' element={<Customers />} />
              <Route path='/login' element={<Login />} />
              <Route path='/settings' element={<Settings />} />
            </Routes>
          </div>
        )}
      </div>
    )
  }
}

export default App
