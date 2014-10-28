using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cacti.Mvc.Web;
using Moq;

namespace Cacti.Mvc.Web.Test
{
    [TestClass]
    public class EnumDisplayFor
    {

        #region test helpers 

        private static ViewContext MockViewContext(ViewDataDictionary vd)
        {
            Mock<ViewContext> mockViewContext = new Mock<ViewContext>(
                new ControllerContext(
                    new Mock<HttpContextBase>().Object,
                    new RouteData(),
                    new Mock<ControllerBase>().Object
                ),
                new Mock<IView>().Object,
                vd,
                new TempDataDictionary(),
                new StreamWriter(new MemoryStream())
            );

            return mockViewContext.Object;
        }

        private static IViewDataContainer MockViewDataContainer(ViewDataDictionary vd)
        {
            Mock<IViewDataContainer> mockDataContainer = new Mock<IViewDataContainer>();
            mockDataContainer.Setup(c => c.ViewData).Returns(vd);

            return mockDataContainer.Object;
        }

        private static HtmlHelper<MyModel> CreateHtmlHelper(MyModel model)
        {
            var dataDict = new ViewDataDictionary(model);
            var context = MockViewContext(dataDict);
            var container = MockViewDataContainer(dataDict);
            var helper = new HtmlHelper<MyModel>(context, container);
            return helper;
        }

        #endregion

        private enum MyEnumWithDisplayName
        {
            [Display(Name = "My Foo")]
            Foo = 1,
            [Display(Name = "My Bar")]
            Bar = 2
        }

        private enum MyEnumWithoutDisplayName
        {
            Hello = 10,
            ByeBye = 3
        }

        private class MyModel
        {
            public MyEnumWithDisplayName DisplayNameEnum { get; set; }
            public MyEnumWithoutDisplayName VanillaEnum { get; set; }
        }

        [TestMethod]
        public void Enum_With_DisplayName_Returns_Enum_DisplayName()
        {
            var value = MyEnumWithDisplayName.Foo.DisplayFor();
            Assert.AreEqual("My Foo", value);
        }
        
        [TestMethod]
        public void Enum_Without_DisplayName_Returns_Enum_Name()
        {
            var value = MyEnumWithoutDisplayName.Hello.DisplayFor();
            Assert.AreEqual("Hello", value);
        }

        [TestMethod]
        public void Enum_With_DisplayName_Returns_DisplayName_As_IHtmlString()
        {
            var model = new MyModel
            {
                DisplayNameEnum = MyEnumWithDisplayName.Bar
            };

            var helper = CreateHtmlHelper(model);
            var value = helper.DisplayEnumFor(m => m.DisplayNameEnum);

            Assert.AreEqual("My Bar", value.ToHtmlString());
        }

        [TestMethod]
        public void Enum_Without_DisplayName_Returns_Name_As_IHtmlString()
        {
            var model = new MyModel
            {
                VanillaEnum = MyEnumWithoutDisplayName.ByeBye
            };

            var helper = CreateHtmlHelper(model);
            var value = helper.DisplayEnumFor(m => m.VanillaEnum);

            Assert.AreEqual("ByeBye", value.ToHtmlString());
        }
    }
}
