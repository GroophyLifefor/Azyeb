namespace Azyeb.Properties;

public class UTF8
{
    public static int Min { get; } = 0;
    public static int Max { get; } = 65535;
    public static char GetCharOf(int at) => Convert.ToChar(at);
}