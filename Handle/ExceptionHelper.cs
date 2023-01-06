namespace Azyeb.Handle;

internal static class ExceptionHelper
{
    public static bool ThrowIfClassicRuleHasNotTwoValue(string[] value) =>
        (value.Length != 2)
            ? throw new FormatException("Values lower than two or upper than two. (Wrong usage try 'Ayzeb --help')")
            : false;
    
    public static bool ThrowIfValueNotChar(string value) =>
        (value.Length != 1)
            ? throw new FormatException("Values lower than two or upper than two. (Wrong usage try 'Ayzeb --help')")
            : false;
}