using System;

namespace NCas.Common.Exceptions
{
    public abstract class AbstractException:Exception
    {
        public AbstractException() : base()
        {

        }
        public AbstractException(string message) : base(message)
        {
        }
    }
}
