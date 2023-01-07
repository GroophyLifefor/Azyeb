using Azyeb.Handle;
using Newtonsoft.Json;

namespace Azyeb.Properties;

public class Parsing
{
    public class Root
    {
        public List<NewRuleGroup> NewRuleGroup { get; set; }
    }
    
    public class NewRuleGroup
    {
        public string Text { get; set; } = string.Empty;
        public List<Rule> Rules { get; set; } = new List<Rule>();
        public string SaveAs { get; set; } = string.Empty;
        public bool FixDuplicates { get; set; } = false;
        public bool HideEndIdentifiers { get; set; } = false;
    }
    public class Rule
    {
        public string RuleAsString { get; set; }
        public string ValueAfterRuleEnd { get; set; } = string.Empty;
    }
    
    public class RuleArray
    {
        public Parsing.Rule[] Rules { get; set; }
    }
    
    public static Root getByJsonPath(string path) => JsonConvert.DeserializeObject<Root>(File.ReadAllText(path));
        
}