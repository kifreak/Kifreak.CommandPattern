using System.Threading.Tasks;
using Kifreak.CommandPattern.Output;

namespace Kifreak.CommandPattern.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CommandPattern.Execute(args, new OutputConsole());
        }
    }
}
