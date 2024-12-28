using System;
using System.Collections;
using System.Reflection;
using Injector.Core.Logging;


namespace Injector.MacroEx
{
    /// <summary>
    /// Creates Macros from a class attributed with macro attributes.
    /// </summary>
    sealed class MacroSuiteBuilder
    {
        MacroSuiteBuilder() { }

        internal static int Build(object obj, Hashtable macromap)
        {
            Type T; CheckArgs(obj, macromap, out T);

            ILogger logger = ServiceScope.Get<ILogger>();

            MacroSuiteAttribute msa = MacroSuiteAttribute.OnType(T);
            MethodInfo[] methods = GetMethodsOf(T);
            int n = 0;
            foreach (MethodInfo method in methods)
            {
                MacroAttribute ma = MacroAttribute.OnMethod(method);
                if (ma == null) continue;
                if (macromap.ContainsKey(method.Name))
                {
                    logger.Debug("MacroEx: Macro already defined: {0}, skipping.", method.Name);
                    continue;
                }
                if (!IsMethodSignatureOK(method))
                {
                    logger.Debug("MacroEx: Method signature mismatch: {0}, skipping.", method.Name);
                    continue;
                }

                macromap.Add(method.Name, new Macro(obj, method, ma, msa));
                ++n;
            }

            return n;
        }

        internal static int Strip(object obj, Hashtable macromap)
        {
            Type T; CheckArgs(obj, macromap, out T);

            MethodInfo[] methods = GetMethodsOf(T);
            int n = 0;
            foreach (MethodInfo method in methods)
            {
                if (!macromap.ContainsKey(method.Name)) continue;
                macromap.Remove(method.Name);
                ++n;
            }

            return n;
        }

        static bool IsMethodSignatureOK(MethodInfo method)
        {
            if (method.ReturnType != typeof(string)) return false;
            ParameterInfo[] pars = method.GetParameters();
            if (pars.Length != 1 || pars[0].ParameterType != typeof(string)) return false;

            return true;
        }

        [System.Diagnostics.DebuggerHidden]
        static void CheckArgs(object obj, Hashtable macromap, out Type T)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (macromap == null)
                throw new ArgumentNullException("macromap");
            T = obj.GetType();
            if (!MacroSuiteAttribute.IsDefined(T))
                throw new ArgumentException("Not a macro resolution suite", "obj");
        }

        static MethodInfo[] GetMethodsOf(Type T)
        {
            return T.GetMethods(BindingFlags.Instance | BindingFlags.Public);
        }
    }
}
