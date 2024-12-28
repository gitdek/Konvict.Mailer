using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Injector.RateLimit
{

    public enum PatternInfoDirective
    {
        BounceQueue,

        SkipMX,

        ModeNormal,

        ModeBackoff
    }

}
