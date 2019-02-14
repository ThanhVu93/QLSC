using DotNetNuke.Instrumentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace QLHD
{
    /// <summary>
    /// Custom text writer output to log4net
    /// </summary>
    public class Log4netTextWriter : System.IO.TextWriter
    {
        private readonly ILog logger = LoggerSource.Instance.GetLogger(typeof(Log4netTextWriter).FullName);

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public override void Write(string value)
        {
            logger.Debug(value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            Write(new string(buffer, index, count));
        }
    }
}