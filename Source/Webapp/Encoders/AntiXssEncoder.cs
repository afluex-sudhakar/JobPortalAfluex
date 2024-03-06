using Microsoft.Security.Application;
using System.IO;
using System.Web.Util;

namespace Webapp.Encoders
{
    public class AntiXssEncoder : HttpEncoder
    {
        public AntiXssEncoder() { }

        protected override void HtmlEncode(string value, TextWriter output)
        {
            output.Write(AntiXss.HtmlEncode(value));
        }

        protected override void HtmlAttributeEncode(string value, TextWriter output)
        {
            output.Write(AntiXss.HtmlAttributeEncode(value));
        }

        protected override void HtmlDecode(string value, TextWriter output)
        {
            base.HtmlDecode(value, output);
        }

        // Some code omitted but included in the sample
    }
}