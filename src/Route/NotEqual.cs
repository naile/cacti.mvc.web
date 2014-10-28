using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Cacti.Mvc.Web
{
    public class NotEqual : IRouteConstraint
    {
        private readonly string[] _match;

        public NotEqual(string match)
        {
            _match = new[] { match };
        }

        public NotEqual(string[] match)
        {
            _match = match;
        }

        public bool Match(HttpContextBase httpContext,
            Route route,
            string parameterName,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            return _match.All(value => String.Compare(values[parameterName].ToString(),
                value, StringComparison.OrdinalIgnoreCase) != 0);
        }
    }
}
