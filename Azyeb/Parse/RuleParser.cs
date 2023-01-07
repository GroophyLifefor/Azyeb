using Azyeb.Handle;
using Azyeb.Properties;
using Azyeb.Rules;
using Math = Azyeb.Rules.Math;

namespace Azyeb.Parse;

public class RuleParser
{
    public static List<Classic> ParseClassics(List<Parsing.Rule> Rules)
    {
        List<Classic> classicRules = new List<Classic>();
        foreach (var rule in Rules)
        {
            if (rule.RuleAsString.StartsWith('c'))
            {
                //Classic rule
                //c[range, identifier]
                //c[0-9, number]

                var values = rule.RuleAsString
                    .TrimStart('c')              // Remove type declarer
                    .TrimStart('[').TrimEnd(']') // Remove square brackets
                    .Split(',');          // Split by ','

                ExceptionHelper.ThrowIfClassicRuleHasNotTwoValue(values);
                ExceptionHelper.ThrowIfValueNotChar(values[0].Split(':').First());
                ExceptionHelper.ThrowIfValueNotChar(values[0].Split(':').Last());

                var ranges = values[0].Split(':');
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
    
    public static List<Math> ParseMaths(List<Parsing.Rule> Rules)
    {
        List<Math> classicRules = new List<Math>();
        foreach (var rule in Rules)
        {
            if (rule.RuleAsString.StartsWith('m'))
            {
                //Math rule
                //m[range, step, identifier, statement, math]
                //m[0:2, 0.25, number, number mod 0.5 equ 0]
                //0  0.25  0.50  0.75  1  1.25  1.50  1.75  ||  not 2

                var values = rule.RuleAsString
                    .TrimStart('m') // Remove type declarer
                    .TrimStart('[').TrimEnd(']') // Remove square brackets
                    .Split(',') // Split by ','
                    .Select(x => x.Trim()).ToArray();

                ExceptionHelper.ThrowIfClassicRuleHasNotFiveValue(values);
                ExceptionHelper.ThrowIfValueNotDouble(values[0].Split(':').First());
                ExceptionHelper.ThrowIfValueNotDouble(values[0].Split(':')[1]);

                var ranges = values[0].Split(':');
                
                if (string.IsNullOrEmpty(ranges[0])) ranges[0] = "0";
                if (ranges[0].ToLower().Equals("min")) ranges[0] = double.MinValue.ToString();
                if (string.IsNullOrEmpty(ranges[1])) ranges[1] = double.MaxValue.ToString();

                var rangeValues = ranges.ToList().ConvertAll(s => double.Parse(s.Replace('.', ',')));

                RuleGoingType ruleGoingType = rangeValues[0] > rangeValues[1] ? RuleGoingType.Down : RuleGoingType.Up;

                classicRules.Add(new Math()
                {
                    rangeStart = rangeValues[0],
                    rangeEnd = rangeValues[1],
                    step = double.Parse(values[1]),
                    identifier = values[2],
                    statement = values[3].Replace("true", "1").Replace("false", "0"),
                    math = values[4],
                    ruleGoingType = ruleGoingType,
                    ValueAfterRuleEnd = rule.ValueAfterRuleEnd
                });
                classicRules.Last().Init();
            }
        }

        return classicRules;
    }
}