using System.Text;
using Azyeb.Parse;
using Azyeb.Rules;

namespace Azyeb;

public class UnStackableRunner
{
    public static string Run(GroupInstance groupInstance)
    {
        StringBuilder stdout = new StringBuilder();
        
        List<Classic> rules = RuleParser.ParseClassics(groupInstance.ruleGroup.Rules);
        ExecuteClassicRules(rules, groupInstance, ref stdout);
        return stdout.ToString();
    }

    public static void ExecuteClassicRules(List<Classic> rules, GroupInstance groupInstance, ref StringBuilder stdout)
    {
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

            string text = groupInstance.ruleGroup.Text;
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
    }
}