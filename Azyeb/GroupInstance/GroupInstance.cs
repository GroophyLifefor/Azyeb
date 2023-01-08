using Azyeb.Properties;
using Newtonsoft.Json;

namespace Azyeb;

public class GroupInstance
{
    public Parsing.NewRuleGroup ruleGroup { get; set; } = new Parsing.NewRuleGroup();


    /// <summary>
    /// Change Text of RuleGroup
    /// </summary>
    /// <param name="text"></param>
    public void LoadText(string text) => ruleGroup.Text = text;
    
    /// <summary>
    /// Load Rule to RuleGroup
    /// </summary>
    /// <param name="rule"></param>
    public void LoadRule(Parsing.Rule rule) => ruleGroup.Rules.Add(rule);
    
    /// <summary>
    /// Load Rule foreach item to RuleGroup
    /// </summary>
    /// <param name="rules"></param>
    public void LoadRules(Parsing.Rule[] rules) { 
        foreach (var rule in rules) {
            LoadRule(rule); } }
    
    /// <summary>
    /// Load Rule foreach item to RuleGroup
    /// </summary>
    /// <param name="rules"></param>
    public void LoadRules(List<Parsing.Rule> rules) => rules.ForEach(x => LoadRule(x));
    
    /// <summary>
    /// Load Rule foreach item of JSON item
    /// </summary>
    /// <param name="path"></param>
    public void LoadRulesFromJsonFile(string path)
    {
        var rules = JsonConvert.DeserializeObject<Parsing.RuleArray>(
                File.ReadAllText(path))
            ?.Rules;
        if (rules is not null)
            LoadRules(
                rules
            );
    }
    
    /// <summary>
    /// Return JSON from this RuleGroup
    /// </summary>
    /// <param name="path"></param>
    public string GetRulesAsJsonFile() => JsonConvert.SerializeObject(ruleGroup, Formatting.Indented);

    /// <summary>
    /// Load class from JSON
    /// </summary>
    /// <param name="path"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void LoadConfigFromJsonFile(string path)
    {
        ruleGroup = JsonConvert.DeserializeObject<Parsing.Root>(File.ReadAllText(path))?.NewRuleGroup.First() ?? throw new InvalidOperationException();
    }
}

