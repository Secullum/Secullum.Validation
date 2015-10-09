using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secullum.Validation
{
    internal static class ValidationUtils
    {
        public static bool IsCpf(string value)
        {
            value = value.NumbersOnly();

            if (value.Length != 11)
            {
                return false;
            }

            // Test sequences like "99999999999"
            if (value.ToCharArray().All(x => x == value[0]))
            {
                return false;
            }

            string cpfTemp, checkDigits;
            int sum = 0;

            cpfTemp = value.Substring(0, 9);

            //get fisrt digit
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(cpfTemp[i].ToString()) * multiplier1[i];
            }

            var mod = sum % 11;

            checkDigits = (mod < 2 ? 0 : 11 - mod).ToString();

            cpfTemp = cpfTemp + checkDigits;

            sum = 0;

            //get second digit
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(cpfTemp[i].ToString()) * multiplier2[i];
            }

            mod = sum % 11;

            checkDigits = checkDigits + (mod < 2 ? 0 : 11 - mod).ToString();

            return value.EndsWith(checkDigits);
        }
    }
}
