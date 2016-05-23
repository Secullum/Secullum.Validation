using System.Globalization;

namespace Secullum.Validation.Tests
{
    public class BaseTest
    {
        protected void SetCurrentThreadCulture(CultureInfo cultureInfo)
        {
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }
    }
}
