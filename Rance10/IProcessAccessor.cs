using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Rance10ObjectViewer
{
    public interface IProcessAccessor : IDisposable
    {
        int                             ProcessId               { get; }
        IntPtr                          ProcessHandle           { get; }
        uint                            AccessFlags             { get; set; }
        bool                            Valid                   { get; }
        IEnumerable<API.MEMORY_BASIC_INFORMATION_32> Regions    { get; }

        void Open(int pid, uint access = API.PROCESS_ALL_ACCESS);
        void Close();

        MemoryAccessResult WriteMemory(IntPtr addr, byte[] data);
        MemoryAccessResult ReadMemory(IntPtr addr, int size);
        MemoryAccessResult ReadMemory(IntPtr addr, byte[] data, int size);
    }

    public class ProcessAccessor : IProcessAccessor
    {
        public int                      ProcessId               { get; protected set; }
        public IntPtr                   ProcessHandle           { get; protected set; }
        public uint                     AccessFlags             { get; set; }
        public bool                     Valid                   { get { return API.WaitForSingleObject(ProcessHandle, 0) == API.WAIT_TIMEOUT; } }
        
        private bool                    disposed                = false;

        public ProcessAccessor()
        {
            AccessFlags = API.PROCESS_ALL_ACCESS;
        }

        public ProcessAccessor(int pid, uint access = API.PROCESS_ALL_ACCESS)
        {
            Open(pid, access);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed)
                return;

            if(disposing)
            {   // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
            }

            // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
            // TODO: 大きなフィールドを null に設定します。
            Close();

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Open()
        {
            ProcessHandle   = API.OpenProcess(API.PROCESS_ALL_ACCESS, 0, (uint)ProcessId);

            if(ProcessHandle == IntPtr.Zero)
                throw new Win32Exception();
        }

        public void Open(int pid, uint access = API.PROCESS_ALL_ACCESS)
        {
            ProcessId   = pid;
            AccessFlags = access;

            Open();
        }

        public void Close()
        {
            if(ProcessHandle == IntPtr.Zero)
                return;

            API.CloseHandle(ProcessHandle);
            ProcessHandle   = IntPtr.Zero;
        }

        public IEnumerable<API.MEMORY_BASIC_INFORMATION_32> Regions
        {
            get
            {
                var p   = IntPtr.Zero;
                var mbi = new API.MEMORY_BASIC_INFORMATION_32();
                var size= (uint)Marshal.SizeOf(mbi);

                for(;;)
                {
                    var b   = API.VirtualQueryEx(ProcessHandle, p, out mbi, size);

                    if(b == 0)
                        break;

                    yield return mbi;

                    var next    = p.ToInt64() + mbi.RegionSize;

                    if(next >= 0x80000000)
                        break;

                    p= new IntPtr(next);
                }
            }
        }

        public MemoryAccessResult WriteMemory(IntPtr addr, byte[] data)
        {
            var written = 0u;
            var b       = API.WriteProcessMemory(ProcessHandle, addr, data, data.Length, out written);
            var err     = Marshal.GetLastWin32Error();
            var result  = new MemoryAccessResult(b != 0, err, addr, (int)written, 0, data);

            return result;
        }

        public MemoryAccessResult ReadMemory(IntPtr addr, int size)
        {
            return ReadMemory(addr, new byte[size], size);
        }

        public MemoryAccessResult ReadMemory(IntPtr addr, byte[] data, int size)
        {
            var readed  = 0u;
            var b       = API.ReadProcessMemory(ProcessHandle, addr, data, size, out readed);
            var err     = Marshal.GetLastWin32Error();
            var result  = new MemoryAccessResult(b != 0, err, addr, 0, (int)readed, data);

            return result;
        }
    }

    public class MemoryAccessResult
    {
        public bool                     Success                 { get; private set; }
        public int                      Error                   { get; private set; }
        public IntPtr                   Address                 { get; private set; }
        public int                      WrittenSize             { get; private set; }
        public int                      ReadedSize              { get; private set; }
        public byte[]                   Data                    { get; private set; }

        public MemoryAccessResult(bool success, int error, IntPtr addr, int written, int readed, byte[] data)
        {
            Success     = success;
            Error       = error;
            Address     = addr;
            WrittenSize = written;
            ReadedSize  = readed;
            Data        = data;
        }
    }
}
