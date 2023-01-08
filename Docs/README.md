# Azyeb

The Azyeb project is an important project in terms of going beyond its general definition, because it is __useless__ in general, as it is a program whose sole purpose is to output in the format you want in accordance with the rules.
Although this will be the aim of the program in the beginning and I will not deviate from its purpose, it should be known that this project will not stay like this.

## Usages

Have some different usages which gives same result.
Like ConfigFile, C# Code, Executable.

### Config File

```diff
────Root
    │   Text
    │   SaveAs
    │   FixDuplicates
    │   HideEndIdentifiers
    │
    ├───Rules
    │       RuleAsString
    │       ValueAfterRuleEnd
```

For an example of usage 

__config.json__
```json
{
  "Text": "Current number is {number}\r\n",
  "Rules": [
    {
      "RuleAsString": "c[0:9, number]"
    }
  ]
}
```
``Azyeb.Console --loadconfig config.json``

or

``Azyeb.Console --rule "c[0:9, number]" --text "Current number is {number}\r\n"``
```diff
Current number is 0↓
Current number is 1↓
Current number is 2↓
Current number is 3↓
Current number is 4↓
Current number is 5↓
Current number is 6↓
Current number is 7↓
Current number is 8↓
Current number is 9↓
```
`↓ meaning goes to newline.`


## Properties

### Text

The `Text` attribute is the part where you set the output in a way that you can use the variables you define in the rules.

Example usage in Config File

```json
{
  "Text": "Current varriable is {VarriableName}\r\n"
}
```
Example usage in C# project
```csharp
var groupInstance = new Azyeb.GroupInstance();
groupInstance.LoadText("Current varriable is {VarriableName}\r\n");
```

Example usage in Executable (-t or --text)

``Azyeb.Console --text "Current number is {number}\r\n"``

--- 