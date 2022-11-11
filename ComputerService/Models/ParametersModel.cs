using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Models;

public class ParametersModel
{
    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; set; } = 1;

    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 10;

    [FromQuery(Name = "searchString")]
    public string? searchString { get; set; } = null;

    [FromQuery(Name = "asc")]
    public bool? asc { get; set; } = true;
}