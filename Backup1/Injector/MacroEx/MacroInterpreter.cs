using System;
using System.Text;

namespace Injector.MacroEx
{
    /// <summary>
    /// Expands a text containing macros. Uses MacroManager for macro resolution.
    /// </summary>
    public sealed class MacroInterpreter
    {
        const char Prefix = Scanner.Prefix;
        const char OpeningBrace = Scanner.OpeningBrace;
        const char ClosingBrace = Scanner.ClosingBrace;

        MacroInterpreter() { }

        internal static string MacroCallString(string macroIdentifier)
        {
            return Prefix + macroIdentifier + OpeningBrace + ClosingBrace;
        }


        public static string Translate(string expression)
        {
            string content;
            Translate(expression, out content);
            return content;
        }

        static bool Translate(string expression, out string content)
        {
            if (expression == null) { content = ""; return true; }
            if (expression.IndexOf(Prefix) < 0) { content = expression; return true; }

            bool ok = true;

            StringBuilder sb = new StringBuilder(expression.Length);

            Scanner S = new Scanner(expression);
            S.Scan();
            while (S.Sym != Symbol.EndOfInput)
            {
                switch (S.Sym)
                {
                    case Symbol.ArbitraryText:
                        sb.Append(S.Value);
                        S.Scan();
                        break;
                    case Symbol.Macro:
                        string mac = S.Value;
                        //string arg = S.Argument != "" ? Translate(S.Argument) : "";
                        string arg, val = null;
                        if (Translate(S.Argument, out arg))
                        {
                            try
                            {
                                val = MacroManager.Resolve(mac, arg);
                                if (val == null)
                                {
                                    ok = false;
                                    val = "<???" + mac + ">";
                                }
                            }
                            catch (Exception ex)
                            {
                                ok = false;
                                val = string.Format("<!!!{0}: {1}>", mac, ex.Message);
                            }
                        }
                        else
                        {
                            ok = false;
                            val = string.Format(Prefix + "{0}({1})", mac, arg);
                        }
                        sb.Append(val);
                        S.Scan();
                        break;
                }
            }
            content = sb.ToString();
            return ok;
        }
    }


    enum Symbol { EndOfInput, ArbitraryText, Macro };

    sealed class Scanner
    {
        public const char Prefix = '$';
        public const char OpeningBrace = '(';
        public const char ClosingBrace = ')';

        public Symbol Sym;
        public string Value;
        public string Argument;

        const char chNull = char.MinValue;
        char ch = chNull;
        string input;
        int pos = 0;

        public Scanner(string inputString)
        {
            input = inputString != null ? inputString : "";
            pos = 0;
            Read();
        }

        public void Scan()
        {
            Argument = "";
            switch (ch)
            {
                case chNull:
                    Sym = Symbol.EndOfInput;
                    Value = "";
                    break;
                case Prefix:
                    Read();
                    if (ch == Prefix)
                        goto default;
                    ReadMacro();
                    break;
                default:
                    ReadArbitraryText();
                    break;
            }
        }

        void Read()
        {
            if (pos >= input.Length)
                ch = chNull;
            else
                ch = input[pos++];
        }

        void ReadArbitraryText()
        {
            StringBuilder sb = new StringBuilder(0, input.Length);
            do
            {
                sb.Append(ch);
                Read();
            }
            while (ch != chNull && ch != Prefix);
            Sym = Symbol.ArbitraryText;
            Value = sb.ToString();
        }

        void ReadMacro()
        {
            StringBuilder sb = new StringBuilder(0, input.Length);
            while (ch != chNull && char.IsLetterOrDigit(ch))
            {
                sb.Append(ch);
                Read();
            }
            if (ch == OpeningBrace)
            {
                ReadArg();
            }
            Sym = Symbol.Macro;
            Value = sb.ToString();
        }

        void ReadArg()
        {
            StringBuilder sb = new StringBuilder(0, input.Length);
            int n = 1;
            Read();
            while (ch != chNull && n > 0)
            {
                switch (ch)
                {
                    case OpeningBrace:
                        ++n;
                        goto default;
                    case ClosingBrace:
                        --n;
                        if (n == 0) break;
                        goto default;
                    default:
                        sb.Append(ch);
                        Read();
                        break;
                }
            }
            Argument = sb.ToString();
            Read();
        }
    }
}
