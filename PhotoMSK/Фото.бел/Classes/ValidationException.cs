using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fotobel.Classes
{
    [Flags]
    public enum MessageCodes
    {
        NoAction = 0, OkAction = 1, CancelAction = 2
    }

    public class ValidationException : Exception
    {
        public MessageCodes Code { get; set; }

        public ValidationException(string message, MessageCodes code = MessageCodes.NoAction) : base(message)
        {
            Code = code;
        }
    }

    public class AccessValidationExcption : ValidationException
    {
        public AccessValidationExcption() : base("Не достаточно прав для выполнения данной операции, обратитесь к администратору.")
        {
        }
    }
}