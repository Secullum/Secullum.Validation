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
            IsUniqueMessage,
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

            englishDictionary.Add(StringTypes.IsUniqueMessage, "{0} already exists.");
            portugueseDictionary.Add(StringTypes.IsUniqueMessage, "{0} já existe.");
            spanishDictionary.Add(StringTypes.IsUniqueMessage, "{0} ya existe.");

            englishDictionary.Add(StringTypes.InvalidExpressionMessage, "Invalid expression.");
            portugueseDictionary.Add(StringTypes.InvalidExpressionMessage, "Expressão inválida.");
            spanishDictionary.Add(StringTypes.InvalidExpressionMessage, "Expresión no válida.");
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
