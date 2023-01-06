import { useState } from "react";
import { Col, Row } from "react-bootstrap";
import { ArrowLeft, ArrowRight, ArrowUp, Plus } from "react-bootstrap-icons";
import { useLocation, useNavigate } from "react-router-dom";
import RolesService from "../../services/auth/roles";
import "../../style/nav-buttons.css";

export const NavButtons = ({collapseSidebar}) => {
    const navigate = useNavigate();
    const location = useLocation();
    const [visible, setVisible] = useState(false);

    const toggleVisible = () => {
        const scrolled = document.documentElement.scrollTop;
        if (scrolled > 150) {
            setVisible(true);
        } else if (scrolled <= 150) {
            setVisible(false);
        }
    };

    const scrollToTop = () =>{
        window.scrollTo({
            top: 0, 
            behavior: 'smooth'
        });
    };

    window.addEventListener('scroll', toggleVisible);

    const showCreateOrderButton = () => {
        if (location.pathname === "/create-order") {
            return false;
        }
        if (RolesService.isTechnician() === true) {
            return false;
        }
        return true;
    }

  return (
    <div className="navigation-items">
        <Row className="nav-buttons">
            <Col className="nav-btn" onClick={() => {navigate(-1)}}>
                <ArrowLeft size={25} />
            </Col>
            <Col className="nav-btn" onClick={() => {navigate(1)}}>
                <ArrowRight size={25} />
            </Col>
        </Row>
        {showCreateOrderButton() === true && (
            <div className="create-order-btn" onClick={() => navigate("/create-order")}>
                <Plus size={64} />
            </div>
        )}
        {visible === true && (
            <div className="scroll-to-top-btn" onClick={scrollToTop}>
                <ArrowUp size={25} />
            </div>
        )}
    </div>
    
  );
};
