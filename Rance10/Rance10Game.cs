using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Rance10ObjectViewer
{
    public class Rance10 : ProcessAccessor
    {
        [DllImport("NativeUtil", CharSet= CharSet.Ansi)]
        public static extern void DumpModule(string filename, IntPtr hProcess,IntPtr hModule);
        [DllImport("ntdll.dll", SetLastError = false)]
        public static extern IntPtr NtSuspendProcess(IntPtr ProcessHandle);
        [DllImport("ntdll.dll", SetLastError = false)]
        public static extern IntPtr NtResumeProcess(IntPtr ProcessHandle);

        private Rance10(Process p)
        {
            Open(p.Id);
        }

        public void DumpModule(IntPtr addr)
        {
            var dir     = Path.GetDirectoryName(GetType().Assembly.Location);

            DumpModule(Path.Combine(dir, "Rance10.ex"), ProcessHandle, addr);
        }

        public void Suspend()
        {
            NtSuspendProcess(ProcessHandle);
        }

        public void Resume()
        {
            NtResumeProcess(ProcessHandle);
        }

        public Rance10ObjectAnalyzer AnalyzeObjTable()
        {
            var ana     = new Rance10ObjectAnalyzer();

            ana.Analyze(this);

            return ana;
        }

        public static Rance10 Create()
        {
            var processes= Process.GetProcessesByName("Rance10");

            if(processes == null || processes.Length == 0)
                throw new Exception("Process not found");

            var Rance10  = new Rance10(processes[0]);

            return Rance10;
        }
    }
}
