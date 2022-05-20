using System;

namespace DataAccess.Exceptions
{
    public class MemberExistException : Exception
    {
        private String message;

        public MemberExistException(String message)
        {
            this.message = message;
        }
    }
}