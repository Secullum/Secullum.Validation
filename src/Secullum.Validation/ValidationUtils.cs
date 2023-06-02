using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secullum.Validation
{
    public static class ValidationUtils
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

        public static bool IsCnpj(string value)
        {
            value = value.NumbersOnly();

            if (value.Length != 14)
            {
                return false;
            }

            // Test sequences like "99999999999"
            if (value.ToCharArray().All(x => x == value[0]))
            {
                return false;
            }

            int sum = 0, mod;

            var cnpjTemp = value.Substring(0, 12);

            //get fisrt digit
            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(cnpjTemp[i].ToString()) * multiplier1[i];
            }

            mod = sum % 11;

            var checkDigits = (mod < 2 ? 0 : 11 - mod).ToString();

            cnpjTemp = cnpjTemp + checkDigits;

            sum = 0;

            //get second digit
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(cnpjTemp[i].ToString()) * multiplier2[i];
            }

            mod = sum % 11;

            checkDigits = checkDigits + (mod < 2 ? 0 : 11 - mod).ToString();

            return value.EndsWith(checkDigits);
        }

        public static bool IsPis(string value)
        {
            var multiplier = new int[11] { 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var sum = 0;
            int mod;
            int auxParse = 0;

            value = value.Replace("-", "").Replace(".", "").PadLeft(12, '0');

            // Test sequences of 0
            if (value.Replace("0", "").Trim().Length == 0)
            {
                return false;
            }

            for (int i = 0; i < 11; i++)
            {
                if (!int.TryParse(value[i].ToString(), out auxParse))
                {
                    return false;
                }

                sum += int.Parse(value[i].ToString()) * multiplier[i];
            }

            mod = sum % 11;

            mod = mod < 2 ? 0 : 11 - mod;

            return value.EndsWith(mod.ToString());
        }
    }
}
