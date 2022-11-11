using ComputerService.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace ComputerService.Services;

public class UriService : IUriService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UriService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Uri GetPageUri(int pageNumber, int pageSize, string searchString, bool asc, Enum? sortOrder)
    {
        var request = _contextAccessor.HttpContext.Request;
        var baseUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
        var route = _contextAccessor.HttpContext?.Request.Path.ToString();
        var endpointUri = new Uri(string.Concat(baseUri, route));
        var modifiedUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "pageNumber", pageNumber.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pageSize.ToString());
        if (sortOrder != null) modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "sortOrder", sortOrder.ToString());
        if (asc != null) modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "asc", asc.ToString());
        if (searchString != null) modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "searchString", searchString);
        return new Uri(modifiedUri);
    }
}