using System;
using System.IO;
using System.Text;

namespace Rance10ObjectViewer
{
    public class DebugStream : TextWriter
    {
        public static DebugStream       Instance                { get; private set; }
        public Encoding                 encoding    = Encoding.Default;

        public override Encoding        Encoding                { get { return encoding; } }

        static DebugStream()
        {
            Instance    = new DebugStream();
        }

        public override void Write(string value)
        {
            System.Diagnostics.Debug.Write(value);
        }

        public override void WriteLine(string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
        }
    }
}
