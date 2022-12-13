import AuthService from "./auth.service";

const getRole = () => {
  return AuthService.getCurrentUser().data.userData.role;
};

const isAdmin = () => {
  if (getRole() === "Administrator") return true;
  return false;
};

const isReceiver = () => {
  if (getRole() === "Receiver") return true;
  return false;
};

const isTechnician = () => {
  if (getRole() === "Technician") return true;
  return false;
};

const RolesService = {
  isAdmin,
  isReceiver,
  isTechnician,
};

export default RolesService;
