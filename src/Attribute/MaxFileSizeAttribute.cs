using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;

namespace Cacti.Mvc.Web
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        /// <summary>
        /// Max filesize
        /// </summary>
        /// <param name="maxFileSize">max size in bytes</param>
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return true;
            }
            return file.ContentLength <= _maxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(_maxFileSize.ToString(CultureInfo.InvariantCulture));
        }
    }
}
