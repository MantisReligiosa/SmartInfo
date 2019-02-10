using System;

namespace Setup.Exceptions
{
    public class SqlException : Exception
    {
        public SqlException(string message) : base(message)
        {
        }

        public SqlException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
