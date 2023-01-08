using Azyeb.Handle;

namespace Azyeb;

public class Runner
{
    public static string Run(GroupInstance groupInstance)
    {
        ExceptionHelper.ThrowIfAnyRuleNotInclude(groupInstance);
        ExceptionHelper.ThrowIfAnyTextNotInclude(groupInstance);
        
        if (groupInstance.ruleGroup.Rules.Any(x => x.RuleAsString.StartsWith('s')))
            return StackableRunner.Run(groupInstance);
        else return UnStackableRunner.Run(groupInstance);
    }
}