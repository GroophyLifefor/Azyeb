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
            var groupInstance = new Azyeb.GroupInstance();
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
            

            string exec = Azyeb.Runner.Run(groupInstance);
            
            Console.WriteLine(exec);
            //File.WriteAllText("out.log", exec, Encoding.UTF8);
            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}