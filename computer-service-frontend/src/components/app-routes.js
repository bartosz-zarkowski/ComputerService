import { Routes, Route } from "react-router-dom";

import Home from "./home";
import CreateOrder from "./create-order/create-order.js";
import Orders from "./orders/orders";
import UserLogs from "./user-logs/user-logs";
import Users from "./users/users";
import Customers from "./customers/customers";
import Settings from "./settings";
import Register from "./auth/register";
import Login from "./auth/login";
import Order from "./order/order";
import NotFound from "./not-found";
import User from "./users/user";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/home" element={<Home />} />
      <Route path="/create-order" element={<CreateOrder />} />
      <Route path="/orders" element={<Orders />} />
      <Route path="/orders/:orderId" element={<Order />} />
      <Route path="/user-logs" element={<UserLogs />} />
      <Route path="/users" element={<Users />} />
      <Route path="/users/:userId" element={<User />} />
      <Route path="/customers" element={<Customers />} />
      <Route path="/register" element={<Register />} />
      <Route path="/login" element={<Login />} />
      <Route path="/settings" element={<Settings />} />
      <Route path="/not-found" element={<NotFound />} />
      <Route path="/*" element={<Home />} />
    </Routes>
  );
};

export default AppRoutes;
