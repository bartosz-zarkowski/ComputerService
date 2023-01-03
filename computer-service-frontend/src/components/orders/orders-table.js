import React, { useState, useEffect } from "react";
import axios from "axios";
import DataTable from "react-data-table-component";
import AuthHeader from "../../services/auth/auth-header";

import Form from "react-validation/build/form";
import Input from "react-validation/build/input";

import "../../style/data-table.css";
import { List, PencilSquare } from "react-bootstrap-icons";
import { Link } from "react-router-dom";

const API_URL = process.env.REACT_APP_API_URL + "orders";

const columns = [
  {
    name: "Title",
    selector: (row) => `${row.title}`,
    sortable: true,
    sortField: "Title",
    width: "310px"
  },
  {
    name: "Customer",
    selector: (row) =>
      `${row.customer.lastName} ${row.customer.firstName}`,
    sortable: true,
    sortField: "Customer",
    width: "190px",
  },
  {
    name: "Created By",
    selector: (row) =>
      `${row.createUser?.lastName} ${row.createUser?.firstName}`,
    sortable: true,
    sortField: "CreatedBy",
    width: "190px",
  },
  {
    name: "Created At",
    selector: (row) => `${new Date(row.createdAt).toLocaleString([], {year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit'})}`,
    sortable: true,
    sortField: "CreatedAt",
    width: "160px",
    
  },
  {
    name: "Status",
    selector: (row) => `${row.status}`,
    sortable: true,
    sortField: "Status",
    width: "135px"
  },
  {
    name: "Show",
    width: "100px",
    button: true,
    selector: (row) => <Link to={"/orders/" + row.id}><div className="nav-list"><List className="nav-show"></List></div></Link>
  },
  {
    name: "Edit",
    width: "85px",
    button: true,
    selector: (row) => <Link to={"/orders/" + row.id}><div className="nav-list"><PencilSquare className="nav-edit"></PencilSquare></div></Link>
  },
];

const OrdersTable = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(10);
  const [sortColumn, setSortColumn] = useState("CreatedAt");
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
    <div className="table-content">
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
        highlightOnHover
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

export default OrdersTable;
