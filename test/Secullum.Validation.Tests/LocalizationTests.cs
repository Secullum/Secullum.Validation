using Xunit;
using System.Globalization;

namespace Secullum.Validation.Tests
{
    public class LocalizationTests
    {
        [Fact]
        public void Localization_GivenEnglishCulture_ReturnsEnglishErrorMessage()
        {
            SetCurrentThreadCulture(new CultureInfo("en-US"));

            var person = new Person();

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.StartsWith("The field", errors[0].Message);
        }

        [Fact]
        public void Localization_GivenPortugueseCulture_ReturnsPortugueseErrorMessage()
        {
            SetCurrentThreadCulture(new CultureInfo("pt-BR"));

            var person = new Person();

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.StartsWith("O campo", errors[0].Message);
        }

        [Fact]
        public void Localization_GivenSpanishCulture_ReturnsSpanishErrorMessage()
        {
            SetCurrentThreadCulture(new CultureInfo("es-ES"));
            
            var person = new Person();

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.StartsWith("El campo", errors[0].Message);
        }

        [Fact]
        public void Localization_GivenUnknowCulture_ReturnsEnglishErrorMessage()
        {
            SetCurrentThreadCulture(new CultureInfo("aa-AA"));

            var person = new Person();

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.StartsWith("The field", errors[0].Message);
        }

        private void SetCurrentThreadCulture(CultureInfo cultureInfo)
        {
#if DNX451
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
#else
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
#endif
        }
    }
}
