using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Cacti.Mvc.Web.Extension
{
    public static class DropdownGroupForExtension
    {
        public static IHtmlString DropdownGroupFor<TModel, TProperty>(
        this HtmlHelper<TModel> html,
        Expression<Func<TModel, TProperty>> expression,
        IEnumerable<SelectListItem> list,
        string selectedValue,
        string title,
        string id)
        {
            var selectedList = list.Select(x => new SelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Value.Equals(selectedValue)
                ? x.Selected = true
                : x.Selected = false
            });

            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine(string.Format("<label for=\"{0}\">{1}</label>", id, title));
            sb.AppendLine(html.ListBoxFor(expression, selectedList, new { @class = "form-control", id }).ToHtmlString());
            sb.AppendLine(html.ValidationMessageFor(expression).ToHtmlString());
            sb.AppendLine("</div>");
            return new HtmlString(sb.ToString());
        }
    }
}
