using System.Globalization;

namespace Secullum.Validation.Tests
{
    public class BaseTest
    {
        protected void SetCurrentThreadCulture(CultureInfo cultureInfo)
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
