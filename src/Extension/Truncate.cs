using System.Web;
using System.Web.Mvc;

namespace Cacti.Mvc.Web
{
    public static class TruncateExtension
    {
        public static IHtmlString Truncate(this HtmlHelper helper, string input, int length, string marker = "...")
        {
            return input.Length <= length
                ? MvcHtmlString.Create(input)
                : MvcHtmlString.Create(input.Substring(0, length) + marker);
        }
    }
}
