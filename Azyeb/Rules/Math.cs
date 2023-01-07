using Mathos.Parser;

namespace Azyeb.Rules;

public class Math
{
    public double rangeStart { get; set; }
    public double rangeEnd { get; set; }
    public double step { get; set; } = 1;
    public string math { get; set; }
    public string statement { get; set; }
    public string identifier { get; set; }
    public RuleGoingType ruleGoingType { get; set; }
    public string ValueAfterRuleEnd { get; set; }
    
    private double youAt { get; set; }
    private double result { get; set; } = 0;
    private bool init = true;
    public void Init()
    {
        youAt = rangeStart;
        parser.LocalVariables.Add(identifier, youAt);
        parser.LocalFunctions.Add("equ", delegate (double[] inputs)
        {
            if (inputs[0].Equals(inputs[1])) return 1;
            else return 0;
        });
            
        parser.Operators.Add("equ", (left, right) => left.Equals(right) ? 1 : 0);
        parser.Operators.Add("neq", (left, right) => left.Equals(right) ? 0 : 1);
        parser.Operators.Add("gtr", (left, right) => left > right ? 1 : 0);
        parser.Operators.Add("lss", (left, right) => left < right ? 1 : 0);
        parser.Operators.Add("mod", (left, right) => left % right);
        Next();
    }

    public double GetValue() => result;
    private Mathos.Parser.MathParser parser { get; set; } = new MathParser();
    public bool Next()
    {
        loop: ;
        if (ruleGoingType == RuleGoingType.Down && youAt <= rangeEnd ||
            ruleGoingType == RuleGoingType.Up && youAt >= rangeEnd)
            return false;
        if (init) init = false;
        else youAt =
            (ruleGoingType == RuleGoingType.Down)
                ? youAt - step
                : ruleGoingType == RuleGoingType.Up
                    ? youAt + step
                    : youAt;
        parser.LocalVariables[identifier] = youAt;
        if (System.Math.Round(parser.Parse(statement)) == 1)
        {
            result = parser.Parse(math);
            return true;
        }
        goto loop;
    }
}