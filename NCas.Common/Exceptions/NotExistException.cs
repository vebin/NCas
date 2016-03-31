namespace NCas.Common.Exceptions
{
    public class NotExistException: AbstractException
    {
        public NotExistException() : base()
        {
        }
        public NotExistException(string message) : base(message)
        {
        }
    }
}
