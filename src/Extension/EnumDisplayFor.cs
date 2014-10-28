using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Cacti.Mvc.Web
{
    public static class EnumDisplayFor
    {
        public static string DisplayFor(this Enum value)
        {
            var enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            var member = enumType.GetMember(enumValue)[0];
            var outValue = "";

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attrs.Any())
            {
                var displayAttr = ((DisplayAttribute)attrs[0]);

                outValue = displayAttr.Name;

                if (displayAttr.ResourceType != null)
                {
                    outValue = displayAttr.GetName();
                }
            }
            else
            {
                outValue = value.ToString();
            }

            return outValue;
        }

        public static IHtmlString DisplayEnumFor<TModel, TEnum>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TEnum>> ex)
            where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            var value = ModelMetadata.FromLambdaExpression(ex, html.ViewData).Model;

            var foo = value as Enum;
            if (value == null) throw new ArgumentException("TEnum must be of type Enum");
            return new HtmlString(DisplayFor(foo));
        }
    }
}
