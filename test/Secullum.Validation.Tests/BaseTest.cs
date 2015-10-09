using System.Globalization;

namespace Secullum.Validation.Tests
{
    public class BaseTest
    {
        protected void SetCurrentThreadCulture(CultureInfo cultureInfo)
        {
#if DNXCORE50
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
#else
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
#endif
        }
    }
}
