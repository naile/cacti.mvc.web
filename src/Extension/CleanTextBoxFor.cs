using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Cacti.Mvc.Web
{
    /// <summary>
    /// Same as TextBoxFor but with spellcheck=false, autocomplete=off and autocapitalize=off
    /// </summary>
    public static class CleanTextBoxForExtension
    {
        private static IDictionary<string, object> SetHtmlAttributes(object htmlAttributes)
        {
            if(htmlAttributes == null)
                htmlAttributes = new object();
            var dict = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            dict.Add("spellcheck", "false");
            dict.Add("autocomplete", "off");
            dict.Add("autocapitalize", "off");

            return dict;
        }

        public static MvcHtmlString CleanTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            return CleanTextBoxFor(htmlHelper, expression, null, null);
        }

        public static MvcHtmlString CleanTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string format)
        {
            return CleanTextBoxFor(htmlHelper, expression, format, null);
        }

        public static MvcHtmlString CleanTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return CleanTextBoxFor(htmlHelper, expression, null, htmlAttributes);
        }

        public static MvcHtmlString CleanTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string format, object htmlAttributes)
        {
            var attributes = SetHtmlAttributes(htmlAttributes);

            return format == null
                ? System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, attributes)
                : System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, format, attributes);
        }
    }
}
