namespace Azyeb;

public class ArgumentParsing
{
    private List<(string ShortKey, string LongKey, Action<string> action)> Arguments =
        new List<(string ShortKey, string LongKey, Action<string> action)>();

    public void AddArgument(string ShortKey, string LongKey, Action<string> action) => Arguments.Add((ShortKey.toLower(), LongKey.toLower(), action));

    public void Parse(string[] args)
    {
        var Key = string.Empty;
        var Value = string.Empty;
        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];

            if (arg.StartsWith('-'))
            {
                if (!string.IsNullOrEmpty(Key) && Arguments.Any(x => x.ShortKey == Key || x.LongKey == Key))
                    Arguments.Where(x => x.ShortKey == Key || x.LongKey == Key).First().action.Invoke(Value);

                Key = arg.TrimStart('-').toLower();
                Value = string.Empty;
            }
            else Value = arg;
            
        }
        if (!string.IsNullOrEmpty(Key) && Arguments.Any(x => x.ShortKey == Key || x.LongKey == Key))
            Arguments.Where(x => x.ShortKey == Key || x.LongKey == Key).First().action.Invoke(Value);
    }
}

public static class Extentions
{
    public static string toLower(this string value) => value.ToLower().Replace('ı', 'i');
}