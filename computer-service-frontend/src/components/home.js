import React from "react";
import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { Row, Col } from "react-bootstrap";

const Home = () => {
  const { user: currentUser } = useSelector((state) => state.auth);

  if (!currentUser) {
    return <Navigate to="/login" />;
  }

  return (
    <div className="container-fluid bd-content mt-5">
      <div className="home-content">
        <Row className="mt-5">
          <Col>
            <h2>
              <center>
                <b>TEMAT PRACY:</b> System do zarządzania serwisem komputerowym
              </center>
            </h2>
          </Col>
        </Row>
        <Row className="mt-3">
          <Col>
            <h3>
              <center>
                Temat pracy w języku angielskim: Computer service management
                system
              </center>
            </h3>
          </Col>
        </Row>
        <Row className="mt-5 d-flex p-2">
          <Col>
            <b>Cel pracy</b>: Celem pracy jest opracowanie architektury, projektu oraz
            implementacja systemu wspomagającego zarządzanie serwisem
            komputerowym. Główne funkcje systemu to: przyjmowanie zleceń
            serwisowych, monitorowanie przebiegu realizacji zleceń i pracy
            serwisantów oraz łatwy dostęp klienta do informacji o przetwarzanym
            zleceniu.
          </Col>
        </Row>
        <Row className="d-flex p-2 mt-3 ml-3">
          <Col>
          1. Analiza systemu (wymagania wobec systemu, architektura systemu, baza danych).
          </Col>
        </Row>
        <Row className="d-flex p-2 ml-3">
          <Col>
          2. Opracowanie interfejsu systemu z uwzględnieniem zasad użyteczności.
          </Col>
        </Row>
        <Row className="d-flex p-2 ml-3">
          <Col>
          3. Wybór technologii, narzędzi programistycznych oraz algorytmów.
          </Col>
        </Row>
        <Row className="d-flex p-2 ml-3">
          <Col>
          4. Implementacja wybranych funkcjonalności systemu.
          </Col>
        </Row>
      </div>
    </div>
  );
};

export default Home;
