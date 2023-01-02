import React from 'react';
import { MDBFooter } from 'mdb-react-ui-kit';
import { Col, Row } from 'react-bootstrap';
import '../../style/footer.css';
import { useSelector } from 'react-redux';

const Footer = () => {
  const { user: currentUser } = useSelector((state) => state.auth);
  console.log(currentUser)
 return (
    <MDBFooter className='footer'>
      <Row className='footer-items'>
        {!currentUser && (
          <div className="app-version-footer">
          App version: {process.env.REACT_APP_VERSION}
        </div>
        )}
        <Col sm={12} md={12} lg={4} className='footer-logos'>
          <Row className='footer-logo-row'>
            <Col className='footer-logo-col'>
            <img className='footer-logo' src={require("../../images/ZUT_2.png")} alt="User Icon" />
            </Col>
          </Row>
          <Row>
            <Col className='footer-logo-col'>
            <img className='footer-logo' src={require("../../images/WIZUT.png")} alt="User Icon" />
            </Col>
          </Row>
        </Col>
        <Col sm={12} md={12} lg={4} className='title'>
        Computer service management system.
        </Col>
        <Col sm={12} md={12} lg={4} className='informations'>
          <div>
            Bartosz Żarkowski
          </div>
          <div>
            zb46707
          </div>
          <div>
            zb46707@zut.edu.pl
          </div>
        </Col>
      </Row>
  </MDBFooter>
 )
}

export default Footer;