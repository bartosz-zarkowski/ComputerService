import { isEmail } from "validator";

export default class Validation {
  validEmail = (value) => {
    if (!isEmail(value)) {
      return (
        <div className="alert alert-danger" role="alert">
          This is not a valid email.
        </div>
      );
    }
  };

  vFirstName = (value) => {
    if (value.length < 3 || value.length > 20) {
      return (
        <div className="alert alert-danger" role="alert">
          The firstname must be between 3 and 20 characters.
        </div>
      );
    }
  };

  vLastName = (value) => {
    if (value.length < 3 || value.length > 20) {
      return (
        <div className="alert alert-danger" role="alert">
          The lastname must be between 3 and 20 characters.
        </div>
      );
    }
  };

  validPassword = (value) => {
    if (value.length < 8) {
      return (
        <div className="alert alert-danger" role="alert">
          'The password must have at least 8 characters.'
        </div>
      );
    }
    if (!value.match(/[A-Z]/)) {
      return (
        <div className="alert alert-danger" role="alert">
          The password must have at least 1 uppercase character.
        </div>
      );
    }
    if (!value.match(/[a-z]/)) {
      return (
        <div className="alert alert-danger" role="alert">
          The password must have at least 1 lower case character.
        </div>
      );
    }
    if (!value.match(/[\d`~!@#$%\^&*()+=|;:'",.<>\/?\\\-]/)) {
      return (
        <div className="alert alert-danger" role="alert">
          The password must have at least 1 symbol.
        </div>
      );
    }
  };

  vPhoneNumber = (value) => {
    if (value.length < 9 || value.length > 20) {
      return (
        <div className="alert alert-danger" role="alert">
          The phone number must have at least 9 characters.
        </div>
      );
    }
  };

  validRole = (value) => {
    if (value !== "Technician") {
      return (
        <div className="alert alert-danger" role="alert">
          The role must be: Technician, Receiver, Administrator.
        </div>
      );
    }
  };
}
