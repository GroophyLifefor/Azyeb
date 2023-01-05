using System.Text;
using Azyeb.Handle;

namespace Azyeb;

public class Classic
{
    public int rangeStart { get; set; }
    public int rangeEnd { get; set; }
    public string identifier { get; set; }
    public RuleGoingType ruleGoingType { get; set; }
    public string ValueAfterRuleEnd { get; set; }
    
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
        List<Classic> rules = new List<Classic>();

        foreach (var rule in root.Rules)
        {
            if (rule.RuleAsString.StartsWith('k'))
            {
                //Classic rule
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
                        ErrorDesc = "Classic range can not longer than one char. (Wrong usage try 'Ayzeb --help')",
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
                        ErrorDesc = "Classic range can not longer than one char. (Wrong usage try 'Ayzeb --help')",
                        ErrorIndex = 3 + ranges[0].Length,
                        ErrorLen = ranges[1].Length,
                        ErrorLevel = Error.ErrorLevel.Error
                    });
                }

                var rangeChars = ranges.Select(x => (int)Convert.ToChar(x)).ToArray();

                RuleGoingType ruleGoingType = rangeChars[0] > rangeChars[1] ? RuleGoingType.Down : RuleGoingType.Up;

                rules.Add(new Classic()
                {
                    rangeStart = rangeChars[0],
                    rangeEnd = rangeChars[1],
                    identifier = values[1],
                    ruleGoingType = ruleGoingType,
                    ValueAfterRuleEnd = rule.ValueAfterRuleEnd
                });
                rules.Last().Init();
            }
        }

        List<(string key, string value)> hideEndIdentifiersList = new List<(string key, string value)>();
        for (bool first = true;;)
        {
            if (first) first = false;
            else
            {
                for (int i = 0; i < rules.Count; i++)
                {
                    if (!rules[i].Next())
                    {
                        if (groupInstance.ruleGroup.HideEndIdentifiers) hideEndIdentifiersList.Add((rules[i].identifier, rules[i].ValueAfterRuleEnd));
                        rules.RemoveAt(i);
                    }
                } 
                if (rules.Count == 0) break;
            }

            string text = root.Text;
            for (int i = 0; i < rules.Count; i++)
            {
                text = text.Replace($"{{{rules[i].identifier.Trim()}}}", rules[i].GetValue().ToString());
            }

            if (groupInstance.ruleGroup.HideEndIdentifiers)
            {
                foreach (var Identifier in hideEndIdentifiersList)
                {
                    text = text.Replace($"{{{Identifier.key.Trim()}}}", Identifier.value);
                }
            }

            stdout.Append(text);
        }

        return stdout.ToString();
    }
    
}