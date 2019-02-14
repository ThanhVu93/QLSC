using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace QLHD
{
    /// <summary>
    /// Custom text writer output to console
    /// </summary>
    public class ConsoleTextWriter : System.IO.TextWriter
    {
        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }

        public override void Write(string value)
        {
            System.Diagnostics.Debug.Write(value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            Write(new String(buffer, index, count));
        }

    }
}