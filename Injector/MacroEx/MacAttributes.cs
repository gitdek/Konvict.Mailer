using System;
using System.Reflection;

namespace Injector.MacroEx
{
    /// <summary>
    /// Indicates a class that provides macro resolution methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MacroSuiteAttribute : Attribute
    {
        /// <summary>
        /// Checks if this attribute is attached to a given type.
        /// </summary>
        internal static bool IsDefined(Type t)
        {
            return t.IsDefined(typeof(MacroSuiteAttribute), false);
        }

        /// <summary>
        /// Retrieves this attribute on a given type. Returns null if none defined.
        /// </summary>
        internal static MacroSuiteAttribute OnType(Type T)
        {
            foreach (MacroSuiteAttribute a in T.GetCustomAttributes(typeof(MacroSuiteAttribute), false))
                return a;
            return null;
        }

        private string _category = "Misc";
        /// <summary>
        /// Default macro category for a macro suite.
        /// </summary>
        public string Category
        {
            get { return this._category; }
            set { this._category = (value != null && value != "") ? value : "Misc"; }
        }

    }


    /// <summary>
    /// Indicates a method designed for macro resolution.
    /// The method must have the signature 'string methodname(string)'.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class MacroAttribute : Attribute
    {
        private string _caption = "";
        /// <summary>
        /// A human-readable name of the macro; ex.: "Application Directory".
        /// </summary>
        public string Caption
        {
            get { return this._caption; }
            set { this._caption = (value != null) ? value : ""; }
        }

        private string _category = "";
        /// <summary>
        /// Macro category, used for grouping macros.
        /// </summary>
        public string Category
        {
            get { return this._category; }
            set { this._category = (value != null) ? value : ""; }
        }

        private string _argument = "";
        /// <summary>
        /// Name of macro argument, or empty/null if the macro requires no arguments.
        /// Reserved for future use (currently not evaluated).
        /// </summary>
        public string Argument
        {
            get { return this._argument; }
            set { this._argument = (value != null) ? value : ""; }
        }

        /// <summary>
        /// Retrieves this attribute on a given method. Returns null if none defined.
        /// </summary>
        internal static MacroAttribute OnMethod(MethodInfo method)
        {
            foreach (MacroAttribute a in method.GetCustomAttributes(typeof(MacroAttribute), false))
                return a;
            return null;
        }
    }
}
