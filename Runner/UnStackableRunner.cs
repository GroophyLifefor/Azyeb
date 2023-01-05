using System.Text;
using Azyeb.Handle;

namespace Azyeb;

public class Klassic
{
    public int rangeStart { get; set; }
    public int rangeEnd { get; set; }
    public string identifier { get; set; }
    public RuleGoingType ruleGoingType { get; set; }
    
    private int youAt { get; set; }
    public void Init() => youAt = rangeStart;
    public char GetValue() => Convert.ToChar(youAt);

    public bool Next()
    {
        youAt = ruleGoingType == RuleGoingType.Down ? youAt - 1 : youAt + 1;
        return (ruleGoingType == RuleGoingType.Up ? youAt - 1 : youAt) != (ruleGoingType == RuleGoingType.Down ? rangeEnd - 1 : rangeEnd);
    }
}
public class UnStackableRunner
{
    public static string Run(GroupInstance groupInstance)
    {
        var root = groupInstance.ruleGroup;
        StringBuilder stdout = new StringBuilder();
        List<Klassic> rules = new List<Klassic>();

        foreach (var rule in root.Rules)
        {
            if (rule.RuleAsString.StartsWith('k'))
            {
                //Klassic rule
                //k[range, identifier]
                //k[0-9, number]

                var values = rule.RuleAsString
                    .TrimStart('k')              // Remove type declarer
                    .TrimStart('[').TrimEnd(']') // Remove square brackets
                    .Split(',');          // Split by ','

                if (values.Length != 2)
                {
                    Error.PrintError(new Error.ErrorInfo()
                    {
                        ErrorLine = rule.RuleAsString,
                        ErrorDesc = "Values lower than two or upper than two. (Wrong usage try 'Ayzeb --help')",
                        ErrorIndex = 0,
                        ErrorLen = rule.RuleAsString.Length,
                        ErrorLevel = Error.ErrorLevel.Error
                    });
                }

                var ranges = values[0].Split('-');
                if (ranges[0].ToCharArray().Length > 1)
                {
                    Error.PrintError(new Error.ErrorInfo()
                    {
                        ErrorLine = rule.RuleAsString,
                        ErrorDesc = "Klassic range can not longer than one char. (Wrong usage try 'Ayzeb --help')",
                        ErrorIndex = 2,
                        ErrorLen = ranges[0].Length,
                        ErrorLevel = Error.ErrorLevel.Error
                    });
                }
                
                if (ranges[1].ToCharArray().Length > 1)
                {
                    Error.PrintError(new Error.ErrorInfo()
                    {
                        ErrorLine = rule.RuleAsString,
                        ErrorDesc = "Klassic range can not longer than one char. (Wrong usage try 'Ayzeb --help')",
                        ErrorIndex = 3 + ranges[0].Length,
                        ErrorLen = ranges[1].Length,
                        ErrorLevel = Error.ErrorLevel.Error
                    });
                }

                var rangeChars = ranges.Select(x => (int)Convert.ToChar(x)).ToArray();

                RuleGoingType ruleGoingType = rangeChars[0] > rangeChars[1] ? RuleGoingType.Down : RuleGoingType.Up;

                rules.Add(new Klassic()
                {
                    rangeStart = rangeChars[0],
                    rangeEnd = rangeChars[1],
                    identifier = values[1],
                    ruleGoingType = ruleGoingType
                });
                rules.Last().Init();
            }
        }

        for (bool first = true;;)
        {
            if (first) first = false;
            else
            {
                for (int i = 0; i < rules.Count; i++)
                {
                    if (!rules[i].Next())
                        rules.RemoveAt(i);
                } 
                if (rules.Count == 0) break;
            }

            string text = root.Text;
            for (int i = 0; i < rules.Count; i++)
            {
                text = text.Replace("{" + rules[i].identifier.Trim() + "}", rules[i].GetValue().ToString());
            }

            stdout.Append(text);
        }

        return stdout.ToString();
    }
    
}