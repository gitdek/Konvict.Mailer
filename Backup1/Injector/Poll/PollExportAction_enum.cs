using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Injector
{
    /// <summary>
    /// Specifies Poll Export Action
    /// </summary>
    [Flags]
    public enum PollExportAction_enum
    {
        /// <summary>
        /// Output is discarded.
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// A dialog for the server is updated(or created if it does not yet exist) and output is shown in Datagrid. (XML Only)
        /// </summary>
        DisplayDatagrid = 1,

        /// <summary>
        /// A dialog for the server is updated(or created if it does not yet exist) and output is shown in textbox.
        /// </summary>
        DisplayPlaintext = 2,

        /// <summary>
        /// Updates the campaigns overall statistics.
        /// </summary>
        AddToOverallStats = 3,

        /// <summary>
        /// Saves to specified file.
        /// </summary>
        SaveToFile = 4,
    }
}
