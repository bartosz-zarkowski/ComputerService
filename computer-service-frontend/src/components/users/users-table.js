import React, { useState, useEffect } from "react";
import axios from "axios";
import DataTable from "react-data-table-component";
import AuthHeader from "../../services/auth/auth-header";

const API_URL = process.env.REACT_APP_API_URL + "users";

const columns = [
  {
    name: "Name",
    selector: (row) => `${row.firstName + ` ` + row.lastName}`,
    sortable: true,
  },
  {
    name: "Email",
    selector: (row) => `${row.email}`,
    sortable: true,
  },
  {
    name: "Phone Number",
    selector: (row) => `${row.phoneNumber}`,
    sortable: true,
  },
  {
    name: "Role",
    selector: (row) => `${row.role}`,
    sortable: true,
  },
  {
    name: "Active",
    selector: (row) => `${row.isActive}`,
    sortable: true,
  },
];

const UsersTable = () => {
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
    console.log(response.data.data);
    setData(response.data.data);
    setTotalCount(response.data.metadata.totalCount);
    console.log(response.data.metadata.totalCount);
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
      title="Users"
      columns={columns}
      data={data}
      progressPending={loading}
      pagination
      paginationServer
      paginationTotalRows={totalCount}
      paginationDefaultPage={currentPage}
      paginationRowsPerPageOptions={[5, 10]}
      onChangeRowsPerPage={handlePerPageChange}
      onChangePage={handlePageChange}
    />
  );
};

export default UsersTable;
