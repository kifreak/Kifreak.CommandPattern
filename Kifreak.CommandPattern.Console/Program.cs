using System.Threading.Tasks;

namespace Kifreak.CommandPattern.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CommandPattern.Execute(args);
        }
    }
}
