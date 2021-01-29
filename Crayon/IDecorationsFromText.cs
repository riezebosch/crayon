namespace Crayon
{
    public interface IDecorationsFromText
    {
        string Bold(string text);
        string Dim(string text);
        string Underline(string text);
        string Reversed(string text);
    }
}