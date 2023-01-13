namespace Azyeb.Handle;

internal static class ExceptionHelper
{
    public static bool ThrowIfClassicRuleHasNotTwoValue(string[] value) =>
        (value.Length != 2)
            ? throw new FormatException("Values lower than two or upper than two. (Wrong usage try 'Ayzeb --help')")
            : false;
    
    public static bool ThrowIfClassicRuleHasNotFiveValue(string[] value) =>
        (value.Length != 5)
            ? throw new FormatException("Values lower than five or upper than five. (Wrong usage try 'Ayzeb --help')")
            : false;
    
    public static bool ThrowIfValueNotChar(string value) =>
        (value.Length != 1)
            ? throw new FormatException("Value was not char.")
            : false;
    
    public static bool ThrowIfValueNotDouble(string value) =>
        (!double.TryParse(value, out _))
            ? throw new FormatException("Value was not a Double.")
            : false;

    public static bool ThrowIfAnyRuleNotInclude(GroupInstance value) =>
        (value.ruleGroup.Rules.Count == 0)
            ? throw new ArgumentException("Any rule not found.")
            : false;
    
    public static bool ThrowIfAnyTextNotInclude(GroupInstance value) =>
        (string.IsNullOrEmpty(value.ruleGroup.Text))
            ? throw new ArgumentException("Any rule not found.")
            : false;
    
    public static bool ThrowIfNotHaveValue(string[] value) =>
        (value.Length == 0)
            ? throw new FormatException("Stack rule must have value.")
            : false;
    
    public static bool ThrowIfNotHaveIdentifier(bool _throw, string identifier) =>
        (_throw)
            ? throw new FormatException($"You do not have identifier as '{identifier}'.")
            : false;
}