using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Rance10ObjectViewer
{
    using BOOL = Int32;
    using HANDLE = IntPtr;
    using HMODULE = IntPtr;
    using DWORD = UInt32;
    using LPCVOID = IntPtr;

    public class API
    {
        public unsafe struct MEMORY_BASIC_INFORMATION_32
        {
            public DWORD        BaseAddress;
            public DWORD        AllocationBase;
            public PAGE_PROTECT AllocationProtect;
            public DWORD        RegionSize;
            public PAGE_TYPE    State;
            public PAGE_PROTECT Protect;
            public PAGE_TYPE    Type;
        }

        [Flags]
        public enum PAGE_TYPE : uint
        {
　　        MEM_COMMIT              = 0x00001000,
            MEM_FREE                = 0x00010000,
            MEM_RESERVE             = 0x00002000,
            MEM_IMAGE               = 0x01000000,
            MEM_MAPPED              = 0x00040000,
            MEM_PRIVATE             = 0x00020000,
        }

        [Flags]
        public enum PAGE_PROTECT : uint
        {
            ZERO                    = 0,
            PAGE_NOACCESS           = 0x00000001,
            PAGE_READONLY           = 0x00000002,
            PAGE_READWRITE          = 0x00000004,
            PAGE_WRITECOPY          = 0x00000008,
            PAGE_EXECUTE            = 0x00000010,
            PAGE_EXECUTE_READ       = 0x00000020,
            PAGE_EXECUTE_READWRITE  = 0x00000040,
            PAGE_EXECUTE_WRITECOPY  = 0x00000080,
            PAGE_GUARD              = 0x00000100,
            PAGE_NOCACHE            = 0x00000200,
            PAGE_WRITECOMBINE       = 0x00000400,
            MEM_COMMIT              = 0x00001000,
            MEM_RESERVE             = 0x00002000,
            MEM_DECOMMIT            = 0x00004000,
            MEM_RELEASE             = 0x00008000,
            MEM_FREE                = 0x00010000,
            MEM_PRIVATE             = 0x00020000,
            MEM_MAPPED              = 0x00040000,
            MEM_RESET               = 0x00080000,
            MEM_TOP_DOWN            = 0x00100000,
            MEM_WRITE_WATCH         = 0x00200000,
            MEM_PHYSICAL            = 0x00400000,
            MEM_ROTATE              = 0x00800000,
            MEM_LARGE_PAGES         = 0x20000000,
            MEM_4MB_PAGES           = 0x80000000,
            SEC_FILE                = 0x00800000,
            SEC_IMAGE               = 0x01000000,
            SEC_PROTECTED_IMAGE     = 0x02000000,
            SEC_RESERVE             = 0x04000000,
            SEC_COMMIT              = 0x08000000,
            SEC_NOCACHE             = 0x10000000,
            SEC_WRITECOMBINE        = 0x40000000,
            SEC_LARGE_PAGES         = 0x80000000,
          //MEM_IMAGE               = SEC_IMAGE
          //WRITE_WATCH_FLAG_RESET  = 0x00000001
        }


        [DllImport("kernel32.dll", SetLastError= true)] public static extern HMODULE GetModuleHandle(string lpModuleName);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern IntPtr GetProcAddress(HMODULE hModule, string lpModuleName);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern HANDLE OpenProcess(DWORD dwDesiredAccess, BOOL bInheritHandle, DWORD dwProcessId);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern BOOL WriteProcessMemory(HANDLE hProcess, LPCVOID lpBaseAddress, byte[] lpBuffer, int nSize, out uint lpNumberOfBytesWritten);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern BOOL ReadProcessMemory(HANDLE hProcess, LPCVOID lpBaseAddress, byte[] lpBuffer, int nSize, out uint lpNumberOfBytesWritten);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern IntPtr CreateRemoteThread(HANDLE hProcess, IntPtr lpThreadAttributes, int dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, int dwCreationFlags, out int lpThreadId);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern DWORD WaitForSingleObject(HANDLE hHandle, DWORD dwMilliseconds);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern BOOL CloseHandle(HANDLE hObject);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern BOOL GetExitCodeThread(HANDLE hThread, out DWORD code);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern BOOL VirtualFreeEx(HANDLE hProcess, IntPtr lpAddress, DWORD dwSize, DWORD dwFreeType);
        [DllImport("kernel32.dll", SetLastError= true)] public static extern BOOL VirtualQueryEx(HANDLE hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION_32 lpBuffer, DWORD dwLength);
        [DllImport("psapi.dll",    SetLastError= true)] public static extern DWORD GetMappedFileName(HANDLE hProcess, IntPtr lpv, StringBuilder lpFilename, int nSize);

        public const DWORD              PROCESS_ALL_ACCESS              = 0x001F0FFF;
        public const DWORD              PROCESS_CREATE_PROCESS          = 0x0080;
        public const DWORD              PROCESS_CREATE_THREAD           = 0x0002;
        public const DWORD              PROCESS_DUP_HANDLE              = 0x0040;
        public const DWORD              PROCESS_QUERY_INFORMATION       = 0x0400;
        public const DWORD              PROCESS_QUERY_LIMITED_INFORMATION= 0x1000;
        public const DWORD              PROCESS_SET_INFORMATION         = 0x0200;
        public const DWORD              PROCESS_SET_QUOTA               = 0x0100;
        public const DWORD              PROCESS_SUSPEND_RESUME          = 0x0800;
        public const DWORD              PROCESS_TERMINATE               = 0x0001;
        public const DWORD              PROCESS_VM_OPERATION            = 0x0008;
        public const DWORD              PROCESS_VM_READ                 = 0x0010;
        public const DWORD              PROCESS_VM_WRITE                = 0x0020;
      //public const DWORD              SYNCHRONIZE                     = 0x00100000;
      
        public const DWORD              MEM_COMMIT              = 0x1000;
        public const DWORD              MEM_RESERVE             = 0x2000;
        public const DWORD              MEM_FREE                = 0x10000;

        public const DWORD              MEM_MAPPED              = 0x40000;
        public const DWORD              MEM_PRIVATE             = 0x20000;
        public const DWORD              MEM_IMAGE               = 0x1000000;

        public const DWORD              PAGE_EXECUTE            = 0x10;
        public const DWORD              PAGE_EXECUTE_READ       = 0x20;
        public const DWORD              PAGE_EXECUTE_READWRITE  = 0x40;
        public const DWORD              PAGE_EXECUTE_WRITECOPY  = 0x80;
        public const DWORD              PAGE_NOACCESS           = 0x01;
        public const DWORD              PAGE_READONLY           = 0x02;
        public const DWORD              PAGE_READWRITE          = 0x04;
        public const DWORD              PAGE_WRITECOPY          = 0x08;
        public const DWORD              PAGE_GUARD              = 0x100;
        public const DWORD              PAGE_NOCACHE            = 0x200;
        public const DWORD              PAGE_WRITECOMBINE       = 0x400;
      //public const DWORD              PAGE_GUARD              = 0x100;
      //public const DWORD              PAGE_NOCACHE            = 0x200;
      //public const DWORD              PAGE_WRITECOMBINE       = 0x400;
        public const DWORD              PAGE_REVERT_TO_FILE_MAP = 0x80000000;

      //public const DWORD              MEM_COMMIT              = 0x1000;
      //public const DWORD              MEM_RESERVE             = 0x2000;
        public const DWORD              MEM_DECOMMIT            = 0x4000;
        public const DWORD              MEM_RELEASE             = 0x8000;
      //public const DWORD              MEM_FREE                = 0x10000;
      //public const DWORD              MEM_PRIVATE             = 0x20000;
      //public const DWORD              MEM_MAPPED              = 0x40000;
        public const DWORD              MEM_RESET               = 0x80000;
        public const DWORD              MEM_TOP_DOWN            = 0x100000;
        public const DWORD              MEM_WRITE_WATCH         = 0x200000;
        public const DWORD              MEM_PHYSICAL            = 0x400000;
        public const DWORD              MEM_ROTATE              = 0x800000;
        public const DWORD              MEM_DIFFERENT_IMAGE_BASE_OK= 0x800000;
        public const DWORD              MEM_RESET_UNDO          = 0x1000000;
        public const DWORD              MEM_LARGE_PAGES         = 0x20000000;
        public const DWORD              MEM_4MB_PAGES           = 0x80000000;
      //public const DWORD              MEM_IMAGE               = SEC_IMAGE;
        public const DWORD              MEM_UNMAP_WITH_TRANSIENT_BOOST  = 0x01;

        public const DWORD              SEC_FILE                = 0x800000;
        public const DWORD              SEC_IMAGE               = 0x1000000;
        public const DWORD              SEC_PROTECTED_IMAGE     = 0x2000000;
        public const DWORD              SEC_RESERVE             = 0x4000000;
        public const DWORD              SEC_COMMIT              = 0x8000000;
        public const DWORD              SEC_NOCACHE             = 0x10000000;
        public const DWORD              SEC_WRITECOMBINE        = 0x40000000;
        public const DWORD              SEC_LARGE_PAGES         = 0x80000000;
        public const DWORD              SEC_IMAGE_NO_EXECUTE    = SEC_IMAGE | SEC_NOCACHE;

        public const DWORD              INFINITE                = 0xFFFFFFFF;
        public const DWORD              STATUS_WAIT_0           = 0x00000000;
        public const DWORD              WAIT_OBJECT_0           = STATUS_WAIT_0 + 0;
        public const DWORD              WAIT_TIMEOUT            = 258;
    }
}
