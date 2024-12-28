using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace Injector.MacroEx
{
    // TODO: Split macro suites by application domain, ex. PathFunctionMacros, SysInfoMacros ...

    [MacroSuite]
    public sealed class BasicMacros
    {
        public static BasicMacros Suite = new BasicMacros();
        BasicMacros() { }

        [Macro(Category = "Variable", Caption = "Application Executable Path")]
        public string AppPath(string arg)
        {
            return Application.ExecutablePath;
        }

        [Macro(Category = "Variable", Caption = "Windows System Directory")]
        public string WinSysDir(string arg)
        {
            return System.Environment.SystemDirectory;
        }

        [Macro(Category = "Variable", Caption = "Common Configuration Directory")]
        public string SysCfgDir(string arg)
        {
            //if (arg != "") return string.Format("<Unexpected argument \"{0}\">", arg);
            return Application.CommonAppDataPath;
        }

        [Macro(Category = "Variable", Caption = "User Configuration Directory")]
        public string UsrCfgDir(string arg)
        {
            return Application.UserAppDataPath;
        }

        [Macro(Category = "Function", Caption = "Absolute File Path")]
        public string AbsPath(string path)
        {
            return Path.GetFullPath(path);
        }

        [Macro(Category = "Function", Caption = "Directory Part of Path")]
        public string Dir(string path)
        {
            if (path == "") return ""; // prevents exception in the following call
            else return Path.GetDirectoryName(path);
        }

        [Macro(Category = "Function", Caption = "File Name Part of Path")]
        public string File(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        [Macro(Category = "Function", Caption = "File Extension Part of Path")]
        public string Ext(string path)
        {
            return Path.GetExtension(path);
        }

        [Macro(Category = "Variable", Caption = "Newline")]
        public string NL(string arg)
        {
            return "\n";
        }

        [Macro(Category = "Variable", Caption = "Tabulator")]
        public string TAB(string arg)
        {
            return "\t";
        }

        [Macro(Category = "Variable", Caption = "Ticks representing the current date and time")]
        public string Ticks(string arg)
        {
            return DateTime.Now.Ticks.ToString();
        }

        #region Invalid Macro-Methods for Testing
#if DEBUG
        [Macro(Category = "Test", Caption = "This will not be accepted as a macro")]
        public void NoMacro1(string arg)
        {
        }

        [Macro(Category = "Test", Caption = "This will not be accepted as a macro")]
        public string NoMacro2()
        {
            return "";
        }

        [Macro(Category = "Test", Caption = "This will not be accepted as a macro")]
        public string NoMacro3(bool arg)
        {
            return "";
        }

        [Macro(Category = "Test", Caption = "This will not be accepted as a macro")]
        public string NoMacro4(string arg1, string arg2)
        {
            return "";
        }

        [Macro(Category = "Test", Caption = "This will not be accepted as a macro")]
        public string NoMacro5(params string[] args)
        {
            return "";
        }
#endif
        #endregion
    }
}
