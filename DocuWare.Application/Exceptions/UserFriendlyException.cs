using System.Globalization;

namespace DocuWare.Application.Exceptions;

public class UserFriendlyException : ApplicationException
{
    public UserFriendlyException()
    {
    }

    public UserFriendlyException(string message) : base(message)
    {
    }

    public UserFriendlyException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}