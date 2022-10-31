using FluentValidation.Results;

namespace ComputerService.Exceptions;

public class BadRequestException : Exception
{
    public List<string> Errors { get; set; }

    public BadRequestException(string message)
    {
        var error = new List<string>() { message };
        Errors = error;
    }

    public BadRequestException(List<ValidationFailure> validationFailure)
    {
        var errors = new List<string>();
        foreach (var error in validationFailure)
        {
            errors.Add(error.ErrorMessage);
        }
        Errors = errors;
    }
}

