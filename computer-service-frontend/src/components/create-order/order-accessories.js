import { useState } from "react";
import { Button, Card, Col, Row } from "react-bootstrap";
import Input from "react-validation/build/input";
import OrderAccessoryService from "../../services/order-accessory/order-accessory.service";

const OrderAccessories = () => {
  const [accessoryName, setAccessoryName] = useState("");
  const storedAccessories = OrderAccessoryService.GetStoredOrderAccessories();
  const [orderAccesories, setOrderAccessories] = useState(storedAccessories);

  const onChangeAccessoryName = (e) => {
    const accessoryName = e.target.value;
    setAccessoryName(accessoryName);
  };

  const onClickAddAccessory = () => {
    OrderAccessoryService.SetStoredOrderAccessories(accessoryName);
  };

  return (
    <Row>
      <Col>
        <Card.Title>Accessories</Card.Title>
        <Row>
          <Col>
            <Input
              type="text"
              className="form-control"
              label="Accessory Name"
              name="accessoryName"
              value={accessoryName}
              onChange={onChangeAccessoryName}
            />
          </Col>
          <Col>
            <Button onClick={onClickAddAccessory}>Add Accessory</Button>
          </Col>
        </Row>
      </Col>
    </Row>
  );
};

export default OrderAccessories;