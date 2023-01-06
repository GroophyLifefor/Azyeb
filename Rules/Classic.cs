namespace Azyeb.Rules;

public class Classic
{
    public int rangeStart { get; set; }
    public int rangeEnd { get; set; }
    public string identifier { get; set; }
    public RuleGoingType ruleGoingType { get; set; }
    public string ValueAfterRuleEnd { get; set; }
    
    private int youAt { get; set; }
    public void Init() => youAt = rangeStart;
    public char GetValue() => Convert.ToChar(youAt);

    public bool Next()
    {
        youAt = ruleGoingType == RuleGoingType.Down ? youAt - 1 : youAt + 1;
        return (ruleGoingType == RuleGoingType.Up ? youAt - 1 : youAt) != (ruleGoingType == RuleGoingType.Down ? rangeEnd - 1 : rangeEnd);
    }
}