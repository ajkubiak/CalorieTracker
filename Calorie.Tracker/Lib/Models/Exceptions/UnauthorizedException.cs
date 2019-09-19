using System;
namespace Lib.Models.Exceptions
{
    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException()
        {
        }
        public UnauthorizedException(string message) :
            base(message)
        {

        }
    }
}
