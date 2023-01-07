using System.Text;
using Azyeb.Parse;
using Azyeb.Rules;
using Math = Azyeb.Rules.Math;

namespace Azyeb;

public class UnStackableRunner
{
    public static string Run(GroupInstance groupInstance)
    {
        StringBuilder stdout = new StringBuilder();
        
        List<Classic> classicRules = RuleParser.ParseClassics(groupInstance.ruleGroup.Rules);
        List<Math> mathRules = RuleParser.ParseMaths(groupInstance.ruleGroup.Rules);
        ExecuteRules(classicRules, mathRules, groupInstance, ref stdout);
        return stdout.ToString();
    }

    public static void ExecuteRules(List<Classic> rules, List<Math> maths, GroupInstance groupInstance, ref StringBuilder stdout)
    {
        List<(string key, string value)> hideEndIdentifiersList = new List<(string key, string value)>();
        for (bool first = true;;)
        {
            if (first)
            {
                first = false;
            }
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
                for (int i = 0; i < maths.Count; i++)
                {
                    if (!maths[i].Next())
                    {
                        if (groupInstance.ruleGroup.HideEndIdentifiers) hideEndIdentifiersList.Add((maths[i].identifier, maths[i].ValueAfterRuleEnd));
                        maths.RemoveAt(i);
                    }
                } 
                if (rules.Count == 0 && maths.Count == 0) break;
            }

            string text = groupInstance.ruleGroup.Text;
            for (int i = 0; i < rules.Count; i++)
            {
                text = text.Replace($"{{{rules[i].identifier.Trim()}}}", rules[i].GetValue().ToString());
            }
            for (int i = 0; i < maths.Count; i++)
            {
                text = text.Replace($"{{{maths[i].identifier.Trim()}}}", maths[i].GetValue().ToString());
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