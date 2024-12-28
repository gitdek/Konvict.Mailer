
namespace Injector.Utilities.CommandLine
{
    /// <summary>
    /// Sets an option, with arguments.
    /// </summary>
    public interface ICommandLineOptions
    {
        /// <summary>
        /// Sets an option, with arguments.
        /// </summary>
        /// <param name="optionName">The option.</param>
        /// <param name="argument">The argument (can be null).</param>
        void SetOption(string optionName, string argument);

        /// <summary>
        /// Displays the options to console
        /// </summary>
        void DisplayOptions();
    }
}