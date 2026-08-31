using System.Collections.Generic;
using System.Globalization;

namespace Secullum.Validation
{
    internal static class Localization
    {
        public enum StringTypes
        {
            IsRequiredMessage,
            HasMaxLengthMessage,
            IsEmailMessage,
            IsCpfMessage,
            IsCnpjMessage,
            IsPisMessage,
            IsUniqueMessage,
            IsOutOfRange,
            IsOutOfDate,
            IsHourMessage,
            IsTimespanMessage,
            IsCepMessage,
            InvalidExpressionMessage
        }

        private static Dictionary<StringTypes, string> englishDictionary = new Dictionary<StringTypes, string>();
        private static Dictionary<StringTypes, string> portugueseDictionary = new Dictionary<StringTypes, string>();
        private static Dictionary<StringTypes, string> spanishDictionary = new Dictionary<StringTypes, string>();

        static Localization()
        {
            englishDictionary.Add(StringTypes.IsRequiredMessage, "The field {0} is required.");
            portugueseDictionary.Add(StringTypes.IsRequiredMessage, "O campo {0} é obrigatório.");
            spanishDictionary.Add(StringTypes.IsRequiredMessage, "El campo {0} es obligatorio.");

            englishDictionary.Add(StringTypes.HasMaxLengthMessage, "The field {0} must have at most {1} characters.");
            portugueseDictionary.Add(StringTypes.HasMaxLengthMessage, "O campo {0} deve possuir no máximo {1} caracteres.");
            spanishDictionary.Add(StringTypes.HasMaxLengthMessage, "El campo {0} debe poseer máximo {1} caracteres.");
            
            englishDictionary.Add(StringTypes.IsEmailMessage, "{0} invalid.");
            portugueseDictionary.Add(StringTypes.IsEmailMessage, "{0} inválido.");
            spanishDictionary.Add(StringTypes.IsEmailMessage, "{0} no válido.");

            englishDictionary.Add(StringTypes.IsCpfMessage, "{0} invalid.");
            portugueseDictionary.Add(StringTypes.IsCpfMessage, "{0} inválido.");
            spanishDictionary.Add(StringTypes.IsCpfMessage, "{0} no válido.");

            englishDictionary.Add(StringTypes.IsCnpjMessage, "{0} invalid.");
            portugueseDictionary.Add(StringTypes.IsCnpjMessage, "{0} inválido.");
            spanishDictionary.Add(StringTypes.IsCnpjMessage, "{0} no válido.");

            englishDictionary.Add(StringTypes.IsPisMessage, "{0} invalid.");
            portugueseDictionary.Add(StringTypes.IsPisMessage, "{0} inválido.");
            spanishDictionary.Add(StringTypes.IsPisMessage, "{0} no válido.");

            englishDictionary.Add(StringTypes.IsUniqueMessage, "{0} already exists.");
            portugueseDictionary.Add(StringTypes.IsUniqueMessage, "{0} já existe.");
            spanishDictionary.Add(StringTypes.IsUniqueMessage, "{0} ya existe.");

            englishDictionary.Add(StringTypes.IsOutOfRange, "The field {0} must be filled with values between {1} and {2}.");
            portugueseDictionary.Add(StringTypes.IsOutOfRange, "O campo {0} deve ser preenchido com valores entre {1} e {2}.");
            spanishDictionary.Add(StringTypes.IsOutOfRange, "El campo {0} debe ser llenado con valores entre {1} y {2}.");

            englishDictionary.Add(StringTypes.InvalidExpressionMessage, "Invalid expression.");
            portugueseDictionary.Add(StringTypes.InvalidExpressionMessage, "Expressão inválida.");
            spanishDictionary.Add(StringTypes.InvalidExpressionMessage, "Expresión no válida.");

            englishDictionary.Add(StringTypes.IsOutOfDate, "Fill the field {0} correctly.");
            portugueseDictionary.Add(StringTypes.IsOutOfDate, "Preencha o campo {0} corretamente.");
            spanishDictionary.Add(StringTypes.IsOutOfDate, "Rellene el campo {0} correctamente.");

            englishDictionary.Add(StringTypes.IsHourMessage, "Fill the field {0} correctly.");
            portugueseDictionary.Add(StringTypes.IsHourMessage, "Preencha o campo {0} corretamente.");
            spanishDictionary.Add(StringTypes.IsHourMessage, "Rellene el campo {0} correctamente.");

            englishDictionary.Add(StringTypes.IsTimespanMessage, "Fill the field {0} correctly.");
            portugueseDictionary.Add(StringTypes.IsTimespanMessage, "Preencha o campo {0} corretamente.");
            spanishDictionary.Add(StringTypes.IsTimespanMessage, "Rellene el campo {0} correctamente.");

            englishDictionary.Add(StringTypes.IsCepMessage, "Fill the field {0} correctly.");
            portugueseDictionary.Add(StringTypes.IsCepMessage, "Preencha o campo {0} corretamente.");
            spanishDictionary.Add(StringTypes.IsCepMessage, "Rellene el campo {0} correctamente.");
        }

        public static string GetString(StringTypes stringType)
        {
            var currentCulture = CultureInfo.CurrentUICulture.Name.ToLower();

            if (currentCulture.StartsWith("pt"))
            {
                return portugueseDictionary[stringType];
            }
            else if (currentCulture.StartsWith("es"))
            {
                return spanishDictionary[stringType];
            }

            return englishDictionary[stringType];
        }
    }
}
