using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace eyeProject2{
    class AutoHotkey {
        [DllImport("AutoHotkey.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "ahktextdll")]
        private static extern IntPtr ahktextdll([MarshalAs(UnmanagedType.LPWStr)]string script,
           [MarshalAs(UnmanagedType.LPWStr)]string options,
           [MarshalAs(UnmanagedType.LPWStr)]string parameters);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("AutoHotkey.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "ahkExec")]
        private static extern bool ahkExec([MarshalAs(UnmanagedType.LPWStr)]string data);

        private IntPtr handle;

        public AutoHotkey() {
            handle = ahktextdll("", "", "");
        }

        ~AutoHotkey() {
            FreeLibrary(handle);
        }

        public bool Exec(string command) {
            return ahkExec(command);
        }
    }
}
