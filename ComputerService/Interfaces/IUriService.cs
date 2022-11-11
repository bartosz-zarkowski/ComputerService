namespace ComputerService.Interfaces;

public interface IUriService
{
    public Uri GetPageUri(int pageNumber, int pageSize, string searchString, bool asc, Enum? sortOrder);
}