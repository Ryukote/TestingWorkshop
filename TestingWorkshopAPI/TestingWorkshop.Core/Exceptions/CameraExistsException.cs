using System;

namespace TestingWorkshop.Core.Exceptions
{
    public class CameraExistsException : Exception
    {
        public CameraExistsException(string message) : base(message)
        {

        }
    }
}
