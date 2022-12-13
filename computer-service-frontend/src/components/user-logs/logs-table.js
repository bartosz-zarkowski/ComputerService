import React, { useState, useEffect } from "react";
import axios from "axios";
import DataTable from "react-data-table-component";
import AuthHeader from "../../services/auth/auth-header";
import "../../style/data-table.css";

const API_URL = process.env.REACT_APP_API_URL + "userTrackings";

const columns = [
  {
    name: "First name",
    selector: (row) => `${row.firstName}`,
    sortable: true,
  },
  {
    name: "Last name",
    selector: (row) => `${row.lastName}`,
    sortable: true,
  },
  {
    name: "Type",
    selector: (row) => `${row.trackingActionType}`,
    sortable: true,
  },
  {
    name: "Description",
    selector: (row) => `${row.description}`,
    sortable: true,
  },
  {
    name: "Date",
    selector: (row) => `${new Date(row.date).toLocaleString()}`,
    sortable: true,
  },
];

const LogsTable = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(10);
  const [currentPage, setCurrentPage] = useState(1);

  const fetchUserLogs = async (page, size = pageSize) => {
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
    fetchUserLogs(1);
  }, []);

  const handlePageChange = (page) => {
    fetchUserLogs(page);
    setCurrentPage(page);
  };

  const handlePerPageChange = async (newPageSize, page) => {
    fetchUserLogs(page, newPageSize);
    setPageSize(newPageSize);
  };

  return (
    <DataTable
      title="User Logs"
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

export default LogsTable;
