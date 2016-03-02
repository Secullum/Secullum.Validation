using System.Globalization;

namespace Secullum.Validation.Tests
{
    public class BaseTest
    {
        protected void SetCurrentThreadCulture(CultureInfo cultureInfo)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
