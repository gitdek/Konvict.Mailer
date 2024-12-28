using System;
using System.Collections;

namespace Injector.MacroEx
{
    abstract class MacroComparer : IComparer
    {
        protected MacroComparer() { }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            Macro a = (Macro)x;
            Macro b = (Macro)y;
            return this.Compare(a, b);
        }

        #endregion

        protected abstract int Compare(Macro a, Macro b);

        public static MacroComparer ByName = new MacroNameComparer();
        public static MacroComparer ByCaption = new MacroCaptionComparer();
        public static MacroComparer ByCategory = new MacroCategoryComparer();
    }


    sealed class MacroNameComparer : MacroComparer
    {
        protected override int Compare(Macro a, Macro b)
        {
            return string.Compare(a.Identifier, b.Identifier, true);
        }
    }

    sealed class MacroCaptionComparer : MacroComparer
    {
        protected override int Compare(Macro a, Macro b)
        {
            return string.Compare(a.Caption, b.Caption, true);
        }
    }

    sealed class MacroCategoryComparer : MacroComparer
    {
        protected override int Compare(Macro a, Macro b)
        {
            int c = string.Compare(a.Category, b.Category, true);
            if (c == 0)
                c = string.Compare(a.Caption, b.Caption, true);
            return c;
        }
    }
}
