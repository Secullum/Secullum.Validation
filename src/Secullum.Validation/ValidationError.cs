namespace Secullum.Validation
{
    public class ValidationError
    {
        public string Property { get; }
        public string Message { get; }

        public ValidationError(string property, string message)
        {
            Property = property;
            Message = message;
        }
    }
}
