# Azyeb
Azyeb gives you the result you want, in the way you want, by acting with various rules.
This project is really valuable to me and I will continue to develop it.

# Docs

https://groophylifefor.github.io/Azyeb/Docs/

# Example of Usage

```json
{
  "NewRuleGroup": [
	{
	  "Text": "char is {chars}\r\n",
	  "Rules": [
		{
		  "RuleAsString": "c[â-ê,chars]"
		}
	  ],
	  "SaveAs": "",
	  "FixDuplicates": false
	}
  ]
}
```
```csharp
var groupInstance = new Azyeb.GroupInstance();
groupInstance.LoadConfigFromJsonFile("debug.json");
string exec = Azyeb.Runner.Run(groupInstance);
Console.WriteLine(exec);
```
```
char is â↓
char is ã↓
char is ä↓
char is å↓
char is æ↓
char is ç↓
char is è↓
char is é↓
char is ê↓
```
`↓ meaning goes to newline.`