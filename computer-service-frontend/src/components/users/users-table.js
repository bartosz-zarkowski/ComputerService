import React, { useState, useEffect } from "react";
import axios from "axios";
import DataTable from "react-data-table-component";
import AuthHeader from "../../services/auth/auth-header";

import Form from "react-validation/build/form";
import Input from "react-validation/build/input";

import "../../style/data-table.css";

const API_URL = process.env.REACT_APP_API_URL + "users";

const columns = [
  {
    name: "First Name",
    selector: (row) => `${row.firstName}`,
    sortable: true,
    sortField: "FirstName",
  },
  {
    name: "Last Name",
    selector: (row) => `${row.lastName}`,
    sortable: true,
    sortField: "LastName",
  },
  {
    name: "Email",
    selector: (row) => `${row.email}`,
    sortable: true,
    sortField: "Email",
  },
  {
    name: "Phone Number",
    selector: (row) => `${row.phoneNumber}`,
  },
  {
    name: "Role",
    selector: (row) => `${row.role}`,
    sortable: true,
    sortField: "Role",
  },
  {
    name: "Active",
    selector: (row) => `${row.isActive}`,
  },
];

const UsersTable = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(10);
  const [sortColumn, setSortColumn] = useState("LastName");
  const [sortDirection, setSortDirection] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const [searchString, setSearchString] = useState("");

  const fetchData = async (
    Page = currentPage,
    PageSize = pageSize,
    SortDirection = sortDirection,
    OrderBy = sortColumn,
    SearchString = searchString
  ) => {
    setLoading(true);
    const response = await axios.get(
      API_URL +
        `?pageNumber=${Page}&pageSize=${PageSize}&searchString=${SearchString}&asc=${SortDirection}&sortOrder=${OrderBy}`,
      { headers: AuthHeader() }
    );
    setData(response.data.data);
    setTotalCount(response.data.metadata.totalCount);
    setLoading(false);
  };

  useEffect(() => {
    fetchData(1);
  }, []);

  const handlePageChange = async (page) => {
    fetchData(page, pageSize, sortDirection, sortColumn, searchString);
    setCurrentPage(page);
  };

  const handlePerPageChange = async (newPageSize, page) => {
    fetchData(page, newPageSize, sortDirection, sortColumn, searchString);
    setPageSize(newPageSize);
  };

  const handleSortChange = async (newSortColumn, newSortDirection, page) => {
    var orderBy = newSortColumn.sortField;
    var asc = newSortDirection === "asc" ? true : false;
    fetchData(currentPage, pageSize, asc, orderBy, searchString);
    setSortColumn(orderBy);
    setSortDirection(asc);
  };

  const handleSearchStringChange = async () => {
    fetchData(currentPage, pageSize, sortDirection, sortColumn);
  };

  const handleEnterPressed = (e) => {
    if (e.key === "Enter") {
      e.preventDefault();
      const SearchString = e.target.value;
      fetchData(1, pageSize, sortDirection, sortColumn, SearchString);
      setSearchString(SearchString);
    }
  };

  return (
    <div>
      <Form onSubmit={handleSearchStringChange}>
        <div className="form-group">
          <label htmlFor="search">Search</label>
          <Input
            type="search"
            className="search form-control rounded"
            name="search"
            onKeyPress={handleEnterPressed}
          />
        </div>
      </Form>

      <DataTable
        columns={columns}
        data={data}
        onSort={handleSortChange}
        sortServer
        progressPending={loading}
        pagination
        paginationServer
        paginationTotalRows={totalCount}
        paginationDefaultPage={currentPage}
        paginationRowsPerPageOptions={[5, 10, 25]}
        onChangeRowsPerPage={handlePerPageChange}
        onChangePage={handlePageChange}
      />
    </div>
  );
};

export default UsersTable;
