namespace Entities.Exceptions
{
    public abstract class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
    public class DomainDuplicationException : DomainException
    {
        public DomainDuplicationException(string message) : base(message)
        {
        }
    } 
    public class DomainNotFoundException : DomainException
    {
        public DomainNotFoundException(string message) : base(message)
        {
        }
    } 
    public class DomainNotValidException : DomainException
    {
        public DomainNotValidException(string message) : base(message)
        {
        }
    }
}
