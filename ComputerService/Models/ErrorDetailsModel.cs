using System.Text.Json;

namespace ComputerService.Models;

public class ErrorDetailsModel
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
    public string Source { get; set; }
    public string StackTrace { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}