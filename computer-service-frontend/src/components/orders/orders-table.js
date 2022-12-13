import React, { useState, useEffect } from "react";
import axios from "axios";
import DataTable from "react-data-table-component";
import AuthHeader from "../../services/auth/auth-header";
import "../../style/data-table.css";

const API_URL = process.env.REACT_APP_API_URL + "orders";

const columns = [
  {
    name: "Title",
    selector: (row) => `${row.title}`,
    sortable: true,
  },
  {
    name: "Customer",
    selector: (row) =>
      `${row.customer.firstName + ` ` + row.customer.lastName}`,
    sortable: true,
  },
  {
    name: "Created By",
    selector: (row) =>
      `${row.createUser?.firstName + ` ` + row.createUser?.lastName}`,
    sortable: true,
  },
  {
    name: "Created At",
    selector: (row) => `${new Date(row.createdAt).toLocaleString()}`,
    sortable: true,
  },
  {
    name: "Status",
    selector: (row) => `${row.status}`,
    sortable: true,
  },
  {
    name: "Edit",
    selector: (row) => `${row}`,
  },
];

const OrdersTable = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(10);
  const [currentPage, setCurrentPage] = useState(1);

  const fetchOrders = async (page, size = pageSize) => {
    setLoading(true);

    const response = await axios.get(
      API_URL + `?pageNumber=${page}&pageSize=${size}`,
      { headers: AuthHeader() }
    );
    setData(response.data.data);
    setTotalCount(response.data.metadata.totalCount);
    setLoading(false);
  };

  useEffect(() => {
    fetchOrders(1);
  }, []);

  const handlePageChange = (page) => {
    fetchOrders(page);
    setCurrentPage(page);
  };

  const handlePerPageChange = async (newPageSize, page) => {
    fetchOrders(page, newPageSize);
    setPageSize(newPageSize);
  };

  return (
    <DataTable
      title="Orders"
      columns={columns}
      data={data}
      progressPending={loading}
      pagination
      paginationServer
      paginationTotalRows={totalCount}
      paginationDefaultPage={currentPage}
      paginationRowsPerPageOptions={[5, 10, 25]}
      onChangeRowsPerPage={handlePerPageChange}
      onChangePage={handlePageChange}
    />
  );
};

export default OrdersTable;
