using System.Text;
using Azyeb.Parse;
using Azyeb.Properties;
using Azyeb.Rules;
using Math = Azyeb.Rules.Math;

namespace Azyeb;

public class StackableRunner
{
    public static string Run(GroupInstance groupInstance)
    {
        StringBuilder stdout = new StringBuilder();
        
        List<Classic> classicRules = RuleParser.ParseClassics(groupInstance.ruleGroup.Rules);
        List<Math> mathRules = RuleParser.ParseMaths(groupInstance.ruleGroup.Rules);
        List<Stack> stackRules = RuleParser.ParseStacks(groupInstance.ruleGroup.Rules, classicRules, mathRules);
        List<string> resultList = new List<string>();
        List<string> hashes = new List<string>();
        foreach (var stack in stackRules)
        {
            if (stack.isMix)
            {
                int totalCount = stack.classics.Count + stack.maths.Count;
                double count = System.Math.Pow(2, totalCount);
                
                for (int i = 1; i <= count - 1; i++)
                {
                    List<Classic> classicRulesTemp = new List<Classic>();
                    List<Math> mathRulesTemp = new List<Math>();
                    string str = Convert.ToString(i, 2).PadLeft(totalCount, '0');
                    for (int j = 0; j < str.Length; j++)
                    {
                        if (str[j] == '1')
                        {
                            if (j >= stack.classics.Count) mathRulesTemp.Add(stack.maths[j]);
                            else classicRulesTemp.Add(stack.classics[j]);
                        }
                    }
                    ExecuteRules(classicRulesTemp, mathRulesTemp, groupInstance, ref resultList);
                }
            }
            else ExecuteRules(stack.classics, stack.maths, groupInstance, ref resultList);
        }
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
    
    static void GetCombination(List<string> list)
    {
        double count = System.Math.Pow(2, list.Count);
        for (int i = 1; i <= count - 1; i++)
        {
            string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j] == '1')
                {
                    Console.Write(list[j]);
                }
            }
            Console.WriteLine();
        }
    }
    
    public static void ExecuteRules(List<Classic> rules, List<Math> maths, GroupInstance groupInstance, ref List<string> resultList)
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

            resultList.Add(text);
        }
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