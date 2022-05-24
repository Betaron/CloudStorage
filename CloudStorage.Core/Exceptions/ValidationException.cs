namespace CloudStorage.Core.Exceptions;
public class ValidationException : Exception
{
    public readonly string ValidationMessage;

    public ValidationException(string validationMessage)
    {
        ValidationMessage = validationMessage;
    }
}
