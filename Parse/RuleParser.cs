using Azyeb.Handle;
using Azyeb.Properties;
using Azyeb.Rules;

namespace Azyeb.Parse;

public class RuleParser
{
    public static List<Classic> ParseClassics(List<Parsing.Rule> Rules)
    {
        List<Classic> classicRules = new List<Classic>();
        foreach (var rule in Rules)
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

                ExceptionHelper.ThrowIfClassicRuleHasNotTwoValue(values);
                ExceptionHelper.ThrowIfValueNotChar(values[0].Split('-').First());
                ExceptionHelper.ThrowIfValueNotChar(values[0].Split('-').Last());

                var ranges = values[0].Split('-');
                if (string.IsNullOrEmpty(ranges[0])) ranges[0] = UTF8.GetCharOf(UTF8.Min).ToString();
                if (string.IsNullOrEmpty(ranges[1])) ranges[1] = UTF8.GetCharOf(UTF8.Max).ToString();

                var rangeChars = ranges.Select(x => (int)Convert.ToChar(x)).ToArray();

                RuleGoingType ruleGoingType = rangeChars[0] > rangeChars[1] ? RuleGoingType.Down : RuleGoingType.Up;

                classicRules.Add(new Classic()
                {
                    rangeStart = rangeChars[0],
                    rangeEnd = rangeChars[1],
                    identifier = values[1],
                    ruleGoingType = ruleGoingType,
                    ValueAfterRuleEnd = rule.ValueAfterRuleEnd
                });
                classicRules.Last().Init();
            }
        }

        return classicRules;
    }
}