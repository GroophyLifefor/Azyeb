using System;
using Azyeb.Handle;
using Azyeb.Properties;

namespace Azyeb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var groupInstance = new Azyeb.GroupInstance();
            /*
            groupInstance.LoadText("Text");
            groupInstance.ruleGroup.FixDuplicates = true;
            
            groupInstance.LoadRule(new Parsing.Rule()
            {
                RuleAsString = "k[0-9,numebers]"
            });
            groupInstance.LoadRules(new []{new Parsing.Rule()
            {
                RuleAsString = "k[0-9,numebers]",
            }, new Parsing.Rule()
            {
                RuleAsString = "k[0-9,numeberss]"
            }});
            groupInstance.LoadRulesFromJsonFile("jsonpath");
            */
            groupInstance.LoadConfigFromJsonFile("debug.json");
            
            /*
            groupInstance.LoadText("number is {number}\r\n");
            groupInstance.LoadRule(new Parsing.Rule()
            {
                RuleAsString = "k[0-9, number]"
            });
            */

            string exec = Azyeb.Runner.Run(groupInstance);
            
            Console.WriteLine(exec);
            Console.ReadKey();
        }
    }
}