using System;
using System.Collections;
using System.Reflection;
using System.Diagnostics;

namespace Injector.MacroEx
{
    public sealed class MacroManager
    {
        MacroManager() { }

        static MacroManager()
        {
            DefineSuite(BasicMacros.Suite);
        }

        static readonly Hashtable macromap = new Hashtable();

        public static void DefineSuite(object macroSuite)
        {
            MacroSuiteBuilder.Build(macroSuite, macromap);
        }

        public static void UndefineSuite(object macroSuite)
        {
            MacroSuiteBuilder.Strip(macroSuite, macromap);
        }

        internal static string Resolve(string macro, string argument)
        {
            Macro m = (Macro)macromap[macro];
            if (m == null)
                //throw new MacroNotDefinedException(macro);
                return null;
            string result = m.Resolve(argument);
            return result;
        }

        internal static Macro[] Macros
        {
            get
            {
                Macro[] result = new Macro[macromap.Count];
                macromap.Values.CopyTo(result, 0);
                return result;
            }
        }
    }


    #region Exceptions
#if false
   class MacroException : System.ApplicationException
   {
      public MacroException() : base() {}
      public MacroException(string msg) : base(msg) {}
      public MacroException(string msg, System.Exception inner) : base(msg, inner) {}
      protected MacroException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext ctx) : base(info, ctx) {}
   }


   sealed class MacroNotDefinedException : MacroException
   {
      public MacroNotDefinedException(string mac) : base(ErrMsg(mac)) {}
      public MacroNotDefinedException(string mac, System.Exception inner) : base(ErrMsg(mac), inner) {}
      private MacroNotDefinedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext ctx) : base(info, ctx) {}

      static string ErrMsg(string mac)
      {
         return string.Format("Macro not defined: {0}", mac);
      }
   }
#endif
    #endregion


    sealed class Macro
    {
        readonly object obj;
        readonly MethodInfo method;
        readonly MacroAttribute attr;
        readonly string category;

        public Macro(object obj, MethodInfo method, MacroAttribute attr, MacroSuiteAttribute suiteAttr)
        {
            this.attr = attr;
            this.obj = obj;
            this.method = method;
            this.category = attr.Category;
            if (this.category == "")
                this.category = suiteAttr.Category;
        }

        string[] args = new string[1];
        public string Resolve(string argument)
        {
            this.args[0] = argument;
            try
            {
                string result = (string)this.method.Invoke(this.obj, this.args);
                return result;
            }
            catch (TargetInvocationException ex)
            {
                // We need to compile the macro engine as a class library.
                // Then we won't get this stupid fucking error.
                // http://kbalertz.com/828991/Error-Message-Method.aspx
                throw ex.InnerException;
            }
        }

        public string Identifier
        {
            get { return this.method.Name; }
        }

        public string Caption
        {
            get { string s = this.attr.Caption; return (s != "") ? s : this.Identifier; }
        }

        public string Category
        {
            get { return this.category; }
        }

        public override string ToString()
        {
            return this.Identifier;
        }
    }
}
