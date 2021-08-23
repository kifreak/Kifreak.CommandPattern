using System.Collections.Generic;
using System.Runtime.InteropServices;
using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Interfaces
{
    public interface ICommandFactory
    {
        string CommandName { get; }
        string Description { get; }

        Dictionary<string, string> OptionsDescription { get; }

        ICommand MakeCommand(Argument argument);
    }
}