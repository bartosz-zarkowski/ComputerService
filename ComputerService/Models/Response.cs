namespace ComputerService.Models;
public class Response<T>
{
    public Response()
    {

    }

    public Response(T data)
    {
        Data = data;
        Succeeded = true;
        ErrorMessage = null;
    }

    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public string ErrorMessage { get; set; }
}
