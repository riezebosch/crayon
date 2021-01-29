namespace Crayon
{
    public interface IOutput : 
        IColors, 
        IColorsFromText, 
        IRgb,
        IRgbFromText,
        IDecorations, 
        IDecorationsFromText
    {
        IBright Bright { get; }
        IBackground Background { get; }
        string Text(string text);
        IOutput Append(string format);
    }
}