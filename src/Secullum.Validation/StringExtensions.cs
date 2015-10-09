using System.Text;

namespace Secullum.Validation
{
    internal static class StringExtensions
    {
        public static string NumbersOnly(this string value)
        {
            var newValue = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                newValue.Append(char.IsNumber(value[i]) ? value[i].ToString() : "");
            }

            return newValue.ToString();
        }
    }
}
