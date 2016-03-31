namespace NCas.Common.Exceptions
{
    /// <summary>
    /// 不在枚举范围内的错误
    /// </summary>
    public class NotInEnumException: AbstractException
    {
        public NotInEnumException() : base()
        {
        }
        public NotInEnumException(string message) : base(message)
        {
        }
    }
}
