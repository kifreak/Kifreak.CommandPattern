using System.Threading.Tasks;

namespace Kifreak.CommandPattern.Interfaces
{
    public interface ICommand
    {
        Task Execute();

        bool Validate();
    }
}