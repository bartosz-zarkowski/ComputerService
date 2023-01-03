import { useRef, useState } from "react";
import { Card, Col, FormGroup, Row } from "react-bootstrap";
import Input from "react-validation/build/input";
import DeviceService from "../../services/device/device.service";
import OrderService from "../../services/order/order.service";

const required = (value) => {
  if (!value) {
    return (
      <div className="alert alert-danger" role="alert">
        This field is required!
      </div>
    );
  }
};

const Details = () => {
  const form = useRef();
  const checkBtn = useRef();

  const storedOrder = OrderService.GetStoredOrder();
  const orderId = useState(storedOrder.orderId);
  const customerId = useState(storedOrder.customerId);

  const storedOrderDevice = DeviceService.GetStoredOrderDevice();
  const deviceId = useState(storedOrderDevice.deviceId);
  const [deviceName, setDeviceName] = useState(storedOrderDevice.Name);
  const [deviceSerialNumber, setDeviceSerialNumber] = useState(
    storedOrderDevice.serialNumber
  );
  const [devicePassword, setDevicePassword] = useState(
    storedOrderDevice.Password
  );
  const [deviceCondition, setDeviceCondition] = useState(
    storedOrderDevice.Condition
  );
  const [deviceHasWarranty, setDeviceHasWarranty] = useState(
    storedOrderDevice.hasWarranty
  );

  const OnChangeDeviceName = (e) => {
    const deviceName = e.target.value;
    setDeviceName(deviceName);
    DeviceService.SetStoredOrderDevice(
      "",
      deviceName,
      deviceSerialNumber,
      devicePassword,
      deviceCondition,
      deviceHasWarranty,
      customerId,
      orderId
    );
  };

  return (
    <Card className="text-center">
      <Card.Body>
        <Card.Title>
          <h3>Details</h3>
        </Card.Title>
        <Row>
          <Col>
            <Card.Title>Device</Card.Title>
            <FormGroup className="form-control-order">
              <label htmlFor="deviceName">Name</label>
              <Input
                type="text"
                className="form-control"
                name="deviceName"
                value={deviceName}
                onChange={OnChangeDeviceName}
                validations={[required]}
              />
            </FormGroup>
          </Col>
        </Row>
      </Card.Body>
    </Card>
  );
};

export default Details;
