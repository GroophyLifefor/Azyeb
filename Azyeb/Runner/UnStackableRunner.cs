using System.Diagnostics;
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
        List<string> hashes = new List<string>();
        List<string> resultList = ExecuteRules(classicRules, mathRules, groupInstance);
        for (int i = 0; i < resultList.Count; i++)
        {
            if (groupInstance.ruleGroup.FixDuplicates)
            {
                if (!hashes.Any(x => HashString(resultList[i]) == x))
                {
                    stdout.Append(resultList[i]);
                    hashes.Add(HashString(resultList[i]));
                }
            }
            else stdout.Append(resultList[i]);
        }
        return stdout.ToString();
    }
    
    public static List<string> ExecuteRules(List<Classic> rules, List<Math> maths, GroupInstance groupInstance)
    {
        List<string> resultList = new List<string>();
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

            resultList.Add(text);
        }

        return resultList;
    }
    
    static string HashString(string text, string salt = "")
    {
        if (String.IsNullOrEmpty(text))
        {
            return String.Empty;
        }
    
        // Uses SHA256 to create the hash
        using (var sha = new System.Security.Cryptography.SHA256Managed())
        {
            // Convert the string to a byte array first, to be processed
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
            byte[] hashBytes = sha.ComputeHash(textBytes);
        
            // Convert back to a string, removing the '-' that BitConverter adds
            string hash = BitConverter
                .ToString(hashBytes)
                .Replace("-", String.Empty);

            return hash;
        }
    }
}