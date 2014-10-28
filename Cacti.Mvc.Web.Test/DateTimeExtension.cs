using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cacti.Mvc.Web.Test
{
    [TestClass]
    public class DateTimeExtension
    {
        [TestMethod]
        public void DateTime_ToJavascriptTimestamp_Returns_Correct_Value_For_Non_Null_DateTime()
        {
            //2014-05-20 14:30:35 = 1369060235020 milliseconds from unix epoch (January 1, 1970 (midnight UTC/GMT))
            var date = new DateTime(2013, 05, 20, 14, 30, 35, 20);

            Assert.AreEqual(1369060235020, date.ToJavascriptTimestamp());
        }

        [TestMethod]
        public void DateTime_ToUnixTimestamp_Returns_Correct_Value_For_Non_Null_DateTime()
        {
            //2014-05-20 14:30:35 = 1369060235 seconds from unix epoch (January 1, 1970 (midnight UTC/GMT))
            var date = new DateTime(2013, 05, 20, 14, 30, 35, 20);

            Assert.AreEqual(1369060235, date.ToUnixTimestamp());
        }
    }
}
