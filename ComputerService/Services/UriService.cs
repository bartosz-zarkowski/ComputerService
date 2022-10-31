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

    public Uri GetPageUri(int pageNumber, int pageSize)
    {
        var request = _contextAccessor.HttpContext.Request;
        var baseUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
        var route = _contextAccessor.HttpContext?.Request.Path.ToString();
        var endpointUri = new Uri(string.Concat(baseUri, route));
        var modifiedUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "pageNumber", pageNumber.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pageSize.ToString());
        return new Uri(modifiedUri);
    }
}