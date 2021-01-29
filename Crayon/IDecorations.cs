namespace Crayon
{
    public interface IDecorations
    {
        IOutput Bold();
        IOutput Dim();
        IOutput Underline();
        IOutput Reversed();
    }
}