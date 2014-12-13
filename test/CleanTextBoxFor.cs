using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cacti.Mvc.Web.Test
{
    [TestClass]
    public class CleanTextBoxFor
    {
        private HtmlHelper<MyModel> _htmlHelper; 
        public class MyModel
        {
            public string InputField { get; set; }
        }

        [TestInitialize]
        public void Setup()
        {
            _htmlHelper = MvcHelper.GetHtmlHelper<MyModel>(new ViewDataDictionary<MyModel>());
        }

        [TestMethod]
        public void InputField_Is_Rendered_With_Correct_Attributes_With_No_Overloads()
        {
            var actualHtmlString = _htmlHelper.CleanTextBoxFor(x => x.InputField).ToHtmlString();

            Assert.IsTrue(actualHtmlString.Contains("spellcheck=\"false\""));
            Assert.IsTrue(actualHtmlString.Contains("autocomplete=\"off\""));
            Assert.IsTrue(actualHtmlString.Contains("autocapitalize=\"off\""));
        }

        [TestMethod]
        public void InputField_Is_Rendered_With_Correct_Attributes_With_Custom_HtmlAttributes()
        {
            var actualHtmlString = _htmlHelper.CleanTextBoxFor(
                x => x.InputField, null, new
            {
                @class = "myCustomclass"
            }).ToHtmlString();

            Assert.IsTrue(actualHtmlString.Contains("spellcheck=\"false\""));
            Assert.IsTrue(actualHtmlString.Contains("autocomplete=\"off\""));
            Assert.IsTrue(actualHtmlString.Contains("autocapitalize=\"off\""));
            Assert.IsTrue(actualHtmlString.Contains("class=\"myCustomclass\""));
        }

        [TestMethod]
        public void InputField_Is_Rendered_With_Correct_Attributes_With_Custom_Format()
        {
            var actualHtmlString = _htmlHelper.CleanTextBoxFor(
                x => x.InputField, format: "test").ToHtmlString();

            Assert.IsTrue(actualHtmlString.Contains("spellcheck=\"false\""));
            Assert.IsTrue(actualHtmlString.Contains("autocomplete=\"off\""));
            Assert.IsTrue(actualHtmlString.Contains("autocapitalize=\"off\""));
        }
    }
}
