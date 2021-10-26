namespace Secullum.Validation
{
    public class ValidationError
    {
        private string _property;

        public static bool CamelCase { get; set; }

        public string Property
        {
            get
            {
                return _property;
            }
            set
            {
                if (CamelCase)
                {
                    _property = string.IsNullOrEmpty(value)
                        ? null
                        : value.Substring(0, 1).ToLower() + value.Substring(1);
                }
                else
                {
                    _property = value;
                }
            }
        }

        public string Message { get; set; }

        public object Data { get; set; }

        public ValidationError(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public ValidationError(string property, string message, object data)
        {
            Property = property;
            Message = message;
            Data = data;
        }
    }
}
