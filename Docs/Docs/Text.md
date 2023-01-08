#### Text

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
