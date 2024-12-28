using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Injector.RateLimit
{
    public class SMTP_PatternInfo
    {
        private string _pattern = "";
        private PatternInfoDirective _directive = PatternInfoDirective.ModeNormal;

        public SMTP_PatternInfo(string pattern, PatternInfoDirective directive)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException("pattern");
            }

            _pattern = pattern;
            _directive = directive;

        }

        #region Properties implementation


        public PatternInfoDirective Directive
        {
            get { return _directive; }
        }

        public string Pattern
        {
            get { return _pattern; }
        }

        #endregion

    }

}
