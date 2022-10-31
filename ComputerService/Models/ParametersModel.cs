using Microsoft.AspNetCore.Mvc;

namespace DF.Ordering.Models;

public class ParametersModel
{
    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; set; } = 1;

    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 10;
}