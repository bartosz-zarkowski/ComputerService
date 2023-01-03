import { Routes, Route } from "react-router-dom";

import Login from "./auth/login";
import Home from "./home";
import CreateOrder from "./create-order/create-order.js";
import Orders from "./orders/orders";
import Order from "./order/order";
import Customers from "./customers/customers";
import Customer from "./customer/customer";
import Register from "./auth/register";
import Users from "./users/users";
import User from "./user/user";
import UserLogs from "./user-logs/user-logs";
import Settings from "./settings/settings";
import NotFound from "./not-found";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/" element={<Home />} />
      <Route path="/*" element={<Home />} />
      <Route path="/home" element={<Home />} />
      <Route path="/create-order" element={<CreateOrder />} />
      <Route path="/orders" element={<Orders />} />
      <Route path="/orders/:orderId" element={<Order />} />
      <Route path="/customers" element={<Customers />} />
      <Route path="/customers/:customerId" element={<Customer />} />
      <Route path="/register" element={<Register />} />
      <Route path="/users" element={<Users />} />
      <Route path="/users/:userId" element={<User />} />
      <Route path="/user-logs" element={<UserLogs />} />
      <Route path="/settings" element={<Settings />} />
      <Route path="/not-found" element={<NotFound />} />
    </Routes>
  );
};

export default AppRoutes;
