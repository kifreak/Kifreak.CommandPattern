namespace Kifreak.CommandPattern.Output
{
    public interface IOutput
    {
        void Info(string message);
        void Color(string message, string color);
        void Warning(string message);
        void Error(string message);
        void EmptyLine(int numberOfLines);
        void Separator();

    }
}