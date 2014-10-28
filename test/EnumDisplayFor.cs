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

        private enum MyEnum
        {
            [Display(Name = "My Foo")]
            Foo = 1,
            [Display(Name = "My Bar")]
            Bar = 2
        }

        private class MyModel
        {
            public MyEnum MyEnum { get; set; }
        }

        [TestMethod]
        public void MyEnum_Foo_Returns_My_Foo_DisplayString()
        {
            var value = MyEnum.Foo.DisplayFor();
            Assert.AreEqual("My Foo", value);
        }

        [TestMethod]
        public void DisplayFor_Enum_Extension_Returns_My_Bar_DisplayName_As_IHtmlString()
        {
            var model = new MyModel
            {
                MyEnum = MyEnum.Bar
            };

            var helper = CreateHtmlHelper(model);
            var value = helper.DisplayEnumFor(m => m.MyEnum);

            Assert.AreEqual("My Bar", value.ToHtmlString());
        }
    }
}
