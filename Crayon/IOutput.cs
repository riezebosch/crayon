namespace Crayon
{
    public interface IOutput
    {
        IOutput Black();
        IOutput Red();
        IOutput Green();
        IOutput Yellow();
        IOutput Blue();
        IOutput Magenta();
        IOutput Cyan();
        IOutput White();
        IOutput Bold();
        IOutput Underline();
        IOutput Reversed();
        string Text(string input);
        IOutput FromRgb(byte r, byte g, byte b);
    }
}