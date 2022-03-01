using System;

namespace TestingWorkshop.Core.Exceptions
{
    public class LensExistsException : Exception
    {
        public LensExistsException(string message) : base(message)
        {

        }
    }
}
