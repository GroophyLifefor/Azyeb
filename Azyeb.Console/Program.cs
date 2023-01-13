using System;
using System.Diagnostics;
using System.Text;
using Azyeb.Handle;
using Azyeb.Properties;
using Math = Azyeb.Rules.Math;

namespace Azyeb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            args = new[]
            {
                "--rule",
                    "m[-5:5, 1, number, true, number ^ 2]",
                "--rule",
                    "m[5:-5, 1, numbers, true, numbers * 2]",
                "--rule",
                    "s[mix, number, numbers]",
                "--fixduplicate",
                "--text",
                    "number is {number} and {numbers}\r\n"
            };
            */
            
            var groupInstance = new Azyeb.GroupInstance();
            
            ArgumentParsing parsing = new ArgumentParsing();
            parsing.AddArgument("t","text", value =>
            {
                groupInstance.LoadText(value.Replace("\\r", "\r").Replace("\\n", "\n"));
            });
            parsing.AddArgument("r","rule", value =>
            {
                groupInstance.LoadRule(new Parsing.Rule()
                {
                    RuleAsString = value
                });
            });
            parsing.AddArgument("lr","loadrules", value =>
            {
                groupInstance.LoadRulesFromJsonFile(value);
            });
            parsing.AddArgument("vae","valueafterend", value =>
            {
                if (groupInstance.ruleGroup.Rules.Count > 0)
                    groupInstance.ruleGroup.Rules.Last().ValueAfterRuleEnd = value;
            });
            parsing.AddArgument("hei","hideendidentifiers", value =>
            {
                groupInstance.ruleGroup.HideEndIdentifiers = true;
            });
            parsing.AddArgument("fixduplicate","fixduplicates", value =>
            {
                groupInstance.ruleGroup.FixDuplicates = true;
            });
            parsing.AddArgument("lc","loadconfig", value =>
            {
                groupInstance.LoadConfigFromJsonFile(value);
            });
            parsing.AddArgument("h", "help", value =>
            {
                
            });
            parsing.Parse(args);
            
            /*
            //groupInstance.LoadConfigFromJsonFile("debug.json");
            groupInstance.LoadText("number {i} is {number}\r\n");
            groupInstance.LoadRule(new Parsing.Rule()
            {
                RuleAsString = "m[-5:9, 1, number, number+1 > 0, number ^ 2]"
            });
            groupInstance.LoadRule(new Parsing.Rule()
            {
                RuleAsString = "c[0:9, i]"
            });
            Azyeb.Console.exe --rule "m[-5:9, 1, number, number+1 > 0, number ^ 2]" --rule "c[0:9, i]" --text "number {i} is {number}\r\n"
            */
            
            Console.WriteLine(groupInstance.GetRulesAsJsonFile());
            
            string exec = Azyeb.Runner.Run(groupInstance);
            
            Console.WriteLine(exec);
            
            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}