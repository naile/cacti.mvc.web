using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Cacti.Mvc.Web
{
    public static class DescriptionForExtension
    {
        public static IHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return DescriptionFor<TModel, TValue>(helper, expression, null);
        }

        public static IHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string descriptionText)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var text = descriptionText ?? metadata.Description;

            if (string.IsNullOrEmpty(text)) return new HtmlString("");

            var tag = new TagBuilder("span");
            tag.SetInnerText(text);

            return new HtmlString(tag.ToString(TagRenderMode.Normal));
        }
    }
}
