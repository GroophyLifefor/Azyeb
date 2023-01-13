namespace Azyeb.Rules;

public class Stack
{
    public List<Classic> classics = new List<Classic>();
    public List<Math> maths = new List<Math>();
    public bool isMix = false;

    public void addStackRule(Classic rule) => classics.Add(rule);
    public void addStackRule(Math rule) => maths.Add(rule);
}