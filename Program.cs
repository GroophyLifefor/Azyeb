using System;
using System.Diagnostics;
using System.Text;
using Azyeb.Handle;
using Azyeb.Properties;
using Mathos.Parser;

namespace Azyeb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MathParser parser = new MathParser();
            parser.LocalFunctions.Add("equ", delegate (double[] inputs)
            {
                if (inputs[0].Equals(inputs[1])) return 1;
                else return 0;
            });
            
            parser.Operators.Add("equ", (left, right) => left.Equals(right) ? 1 : 0);
            parser.Operators.Add("neq", (left, right) => left.Equals(right) ? 0 : 1);
            
            Console.WriteLine(parser.Parse("5+5 == 10"));
            
            return;
            
            var groupInstance = new Azyeb.GroupInstance();
            //groupInstance.LoadConfigFromJsonFile("debug.json");
            
            
            groupInstance.LoadText("number is {number}\r\n");
            groupInstance.LoadRule(new Parsing.Rule()
            {
                RuleAsString = "k[0-9, number]"
            });
            

            string exec = Azyeb.Runner.Run(groupInstance);
            
            Console.WriteLine(exec);
            //File.WriteAllText("out.log", exec, Encoding.UTF8);
            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}