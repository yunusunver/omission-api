using System;

namespace omission.api.Exceptions
{
    public class ServiceException:Exception {
        public ServiceException(string message):base(message)
        {
            
        }
    }
}