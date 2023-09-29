# PhxLib
PHX Lib core utilities and extensions.

## PHX.Debug
Utilities that assist in the debugging of code.

### IDebugDisplay
Provides an interface for a different string representation of an object intended for debugging rather than user display.
```csharp
using System.Diagnostics;
using Phx.Debug;

// System.Diagnostics.DebuggerDisplay attribute controls what is displayed in
// the IDEs debugger view.
[DebuggerDisplay(DebugDisplay.DEBUGGER_DISPLAY_STRING)]
public class MyClass : IDebugDisplay {
    // ToDebugDisplay() method is also available to invoke in debug logging.
    public string ToDebugDisplay() {
        return "MyClass";
    }
    
    public override string ToString() {
        // Or provide a default implementation that is intended for user display.
        return ToDebugDisplay();
    }
}
```

## PHX.Dev
Utilities that assist in development tasks and documentation.

### ToDo
Functions that throws a `NotImplementedException` or `InvalidOperationException`
with that describe the reason the code path is invalid. This is similar to throwing the
`NotImplementedException` or `InvalidOperationException` directly, except it 
"returns" a value to work better with assignment statements and argument passing.
```csharp
using Phx.Dev;
                
var myList = new List<string> {
    ToDo.NotImplementedYet<string>("This parameter need to be computed.")
};

int i = Random.Shared.Next();
switch (i) {
    case 0:
        ToDo.NotImplementedYet("This case needs to be handled.");
        break;
    default:
        ToDo.NotSupportedYet("We haven't figured out how to handle this yet.");
        break;
}
```

### ToDo Attributes
Attributes that document code that needs to be implemented or issues that need
to be resolved but that do not prevent execution. These attributes are similar
to using comments except they are more easily searched and the compiler helps
avoid typos and enforce the presence of descriptions.
```csharp
using Phx.Dev;

[KnownIssue("This class doesn't work all the time.",
    workaround: "Use the NotBroken class instead.",
    link: "www.todo/12345")]
public class BrokenClass { } 

public class TooBigClass {
    [Refactor("This function does too much and can be broken up into component classes.")]
    public void BigFunction() { }
}

[ToDo("This class should optimize its database accesses.", link: "www.todo/67890")]
public class DatabaseClass { }
```

## PHX.Lang
Utilities that add or extend language features.

### ICheckedDisposable
Extends the `IDisposable` interface with a publicly readable boolean property
that indicates if the instance has been disposed.

```csharp
using Phx.Lang;

public class MyResource: ICheckedDisposable {
    public bool IsDisposed { get; private set; }

    public void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing) {
        if(!IsDisposed) {
            if(disposing) {
                // Dispose managed resources.
            }
            IsDisposed = true;
        }
    }
    
    ~MyResource() {
        Dispose(disposing: false);
    }
}
```

### Late.Init
Provides a self documenting way to indicate a non-nullable value will be 
initialized after the constructor is invoked, but before its first access.

This should only be used if it is guaranteed that the value will be set before
it is accessed (via late injection, separate initializer methods, etc).

This is equivalent to using `null!`, but is more clear about the developer's
intentions.

```csharp
using Phx.Lang;

string str = Late.Init<string>();
```

### Optional
Optional can be used as a wrapper around a return value that communicates to the
caller that a value may not be returned. It enforces that that case is handled,
and provides built in helper methods to handle it.

```csharp
using Phx.Lang;

private IOptional<string> GetNickName() {
    return Optional.OfNullable(nickName);
}

private void UseAnOptional() {
    var nickName = GetNickName().OrElse(() => StringUtils.EmptyString);
}
```
Helper methods include:
```csharp
using Phx.Lang;
                
// Create an Optional from a value that may be null.
string? nickName = ReadNickName();
IOptional<string> optionalNickName = Optional.OfNullable(nickName);

// Use Optional.Of() or Optional.EMPTY to explicitly create an optional if it is
// not possible to use Optional.OfNullable().
IOptional<string> result;
try {
    var value = ReadValue();
    result = Optional.Of(value);
} catch (Exception) {
    result = Optional<string>.EMPTY;
}

// Use Optional.If() to create an optional from a boolean condition.
IOptional<int> optional = Optional.If(valueIsSet(), 10);
```
```csharp
using Phx.Lang;

// Perform custom logic based on whether the optional is present
if (optional.IsPresent) {
    // Always check `IsPresent` first as `Value` will throw an exception if the 
    // optional is empty.
    var myValue = optional.Value;
}

if (optional.IsEmpty) {
    return;
}
```
```csharp
using Phx.Lang;

// Functional methods to handle the presence or absence of an optional value.
optional.IfPresent(myValue => { /* ... */ });
optional.IfEmpty(() => { /* ... */ });
```
```csharp
using Phx.Lang;

// Operate on or transform an optional value only if it is present.
IOptional<string> optStr = optional.Map<int, string>(intValue => Optional.Of(intValue.ToString()));
```
```csharp
using Phx.Lang;

// Get default values or try alternatives if an optional is not present.
var v = optional.OrElse(() => 100);

var v2 = optional.OrTry(() => AnotherMethodThatReturnsOptional())
        .OrTry(() => ADifferentMethodThatReturnsOptional())
        .OrThrow(() => new InvalidOperationException("None of the optional values were present."));
```

### Result
The `Result` class allows a method to return a value or indicate failure without
throwing exceptions. This enforces that the error cases are handled, and allows
enumerations of the different error states.

```csharp
using Phx.Lang;

private IResult<string, DatabaseException> ReadDatabaseEntry() {
    return Result.Success<string, DatabaseException>("Entry");
}

private void UseAResult() {
    var entry = ReadDatabaseEntry().OrElse(() => "No Entry");
}
```
The following helper methods are provided:
```csharp
using Phx.Lang;
                
// Create a result from a successful value or exception.
Result.Success(10);
Result.Failure(new Exception());
```
```csharp
using Phx.Lang;

// Check if the result is successful.
var result = GetAValue();
if (result.IsSuccess) {
    var value = (result as Success<string, Exception>)!.Result;
}

if (result.IsFailure) {
    var error = (result as Failure<string, Exception>)!.Error;
}
```
```csharp
using Phx.Lang;

// Check the result for a specific outcome.
if (result.Contains(it => it == "Hello")) { /* ... */ }

if (result.ContainsError(it => it is DatabaseException)) { /* ... */ }
```
```csharp
using Phx.Lang;

// Operate on or transform a result if it is successful.
var stringResult = result.Map(value => value.ToString());

// Operate on or transform a result if it failed.
var newResult = result.MapError(ex => new ParseException(ex));
```
```csharp
using Phx.Lang;

// Get default values or try alternatives if a result failed.
var valueWithAlternative = result.OrElse(() => "Alternative");
var requiredValue = result.OrThrow();
```

### Try.All
Additional utilities for trying multiple actions that may throw exceptions. If
any exceptions were thrown, they are aggregated into a single 
`AggregateException` that is thrown after all actions have been executed. This
is not an asynchronous operation, all actions are executed sequentially.
```csharp
using Phx.Lang;

try {
    Try.All(
        () => { DoFirstThing(); },
        () => { DoSecondThing(); },
        () => { DoThirdThing(); }
    );
} catch (AggregateException ex) {
    foreach (var exception in ex.InnerExceptions) {
        // ...
    }
}

IEnumerable<int> allElements = GetElements();

try {
    Try.All((element) => { 
        DoSomethingWithEachItem(element); 
    }, allElements);
} catch (AggregateException ex) {
    ex.Handle((innerException) => {
        log(innerException);
        return true;
    });
}
```

### Unit
Unit is type that is equivalent to the instantiation of `void`.
It is useful to document that a function will never return a value, or as a way
to pass around function references whose generic parameters require a return 
type.

```csharp
using Phx.Lang;
using static Phx.Lang.Unit;

public Result<Unit, Exception> FunctionWithSideEffect() {
    if (PerformSomeSideEffect()) {
        return Result.Success(UNIT);
    } else {
        return Result.Failure(new Exception());
    } 
}

public Unit AssertFail() {
    throw new Exception(); 
}
```

### String Utils and Extensions
Various string utilities and extensions are also provided.
```csharp
using Phx.Lang;
using static Phx.Lang.StringUtils;

// Constant string values.
public void FunctionWithDefault(string str = EmptyString) {
    // String.Empty is a readonly field and cannot be used as a default argument
    // or parameter in Attributes. EmptyString is a constant and can be used in
    // those cases.
}

public string GetStringValue(string? value) {
    return value?.ToString() ?? NullString;
}
```
```csharp
using Phx.Lang;

// Extension methods for converting objects to string.
object? obj = null;
string str = obj.ToStringSafe();

troublesomeObject.ToDebugDisplayString();
```
```csharp
using Phx.Lang;

// Utilities for inline string building.
var newName = BuildString(sb => {
    sb.Append("First");
    sb.Append("Last");
});
```
```csharp
using Phx.Lang;

// Extension methods for manipulating and escaping strings.
var uppercase = "hello".StartUppercase();
var lowercase = "Hello".StartLowercase();
var implClassName = "IMyInterface".RemoveLeadingI();

var verbatimString = "\"Hello\"".EscapeVerbatimString();
var unescapedVerbatimString = verbatimString.UnescapeVerbatimString();

var quoteString = "\"Hello\"".EscapeStringQuotes();
var unescapedQuoteString = quoteString.UnescapeStringQuotes();
```
```csharp
using Phx.Lang;

// String case conversions
var constantName = "someVariableName".FromCamelCase().ToCapsCase();

if (inputValue.FromPascalCase().IsValid) {
    // ...
}

// Conversions and validations support:
// * camelCase
// * CAPS_CASE
// * kebab-case
// * PascalCase
// * snake_case
```

---

<div align="center">
Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.<br/>
Licensed under the Apache License, Version 2.0.<br/>
See http://www.apache.org/licenses/LICENSE-2.0 for full license information.<br/>
</div>