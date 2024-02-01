# Coding style 

## Naming rules
Valid identifiers must follow these rules. The C# compiler produces an error for any identifier that doesn't follow these rules:
* Identifiers must start with a letter or underscore (_).
* Identifiers can contain Unicode letter characters, decimal digit characters, Unicode connecting characters, Unicode combining characters, or Unicode formatting characters

Please read more [Naming rules (https://learn.microsoft.com/)](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names#naming-rules)

# Naming conventions
In addition to the rules, conventions for identifier names are used throughout the .NET APIs. These conventions provide consistency for names, but the compiler doesn't enforce them. You're free to use different conventions in your projects.

By convention, C# programs use PascalCase for type names, namespaces, and all public members. In addition, the dotnet/docs team uses the following conventions:

* Interface names start with a capital I.
* Attribute types end with the word Attribute.
* Enum types use a singular noun for nonflags, and a plural noun for flags.
* Identifiers shouldn't contain two consecutive underscore (_) characters. Those names are reserved for compiler-generated identifiers.
* Use meaningful and descriptive names for variables, methods, and classes.
* Prefer clarity over brevity.
* Use PascalCase for class names and method names.
* Use camelCase for method arguments, local variables, and private fields.
* Use PascalCase for constant names, both fields and local constants.
* Private instance fields start with an underscore (_).
* Static fields start with s_. This convention isn't the default Visual Studio behavior, nor part of the Framework design guidelines, but is configurable in editorconfig.
* Avoid using abbreviations or acronyms in names, except for widely known and accepted abbreviations.
* Use meaningful and descriptive namespaces that follow the reverse domain name notation.
* Choose assembly names that represent the primary purpose of the assembly.
* Avoid using single-letter names, except for simple loop counters. Also, syntax examples that describe the syntax of C# constructs often use the following single-letter names that match the convention used in the C# language specification. Syntax examples are an exception to the rule.
    * Use S for structs, C for classes.
    * Use M for methods.
    * Use v for variables, p for parameters.
    * Use r for ref parameters.

Please read more [Naming conventions (https://learn.microsoft.com/)](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names#naming-conventions)
