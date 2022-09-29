# PhxLib
PHX Lib core utilities and extensions.

## PHX.Debug
Utilities that assist in the debugging of code.

### IDebugDisplay
Provides an interface for a different string representation of an object intended for debugging rather than user display.
```csharp
using System.Diagnostics;

// System.Diagnostics.DebuggerDisplay attribute controls what is displayed in
// the IDEs debugger view.
[DebuggerDisplay(IDebugDisplay.DEBUGGER_DISPLAY_STRING)]
public class MyClass : IDebugDisplay {

    // ToDebugDisplay() method is also available to invoke in debug logging.
    public string ToDebugDisplay() {
        return "MyClass";
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
var myClass = new MyClass(ToDo.NotImplementedYet("This parameter need to be computed."));
MyClass item = ToDo.NotSupportedYet("We havent figured out how to handle this case.");
```

### ToDo Attributes
Attributes that document code that needs to be implemented or issues that need
to be resolved but that do not prevent execution. These attributes are similar
to using comments except they are more easily searched and the compiler helps
avoid typos and enforce the presence of descriptions.
```csharp
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
public class MyResource: ICheckedDisposable {
    public bool IsDisposed { get; private set; }

    public void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing) {
        if(!this.IsDisposed) {
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
string str = Late.Init<string>();
```

### Optional
Optional can be used as a wrapper around a return value that communicates to the
caller that a value may not be returned. It enforces that that case is handled,
and provides built in helper methods to handle it.

```csharp
private Optional<string> GetNickName() {
    return Optional.OfNullable(this.nickName);
}

private void UseAnOptional() {
    var nickName = GetNickName().OrElse(StringUtils.EmptyString);
}
```
Helper methods include:
```csharp
// Create an Optional from a value that may be null.
string? nickName = ReadNickName();
Optional<string> optionalNickName = Optional.OfNullable(nickName);

// Use Optional.Of() or Optional.EMPTY to explicitly create an optional if it is
// not possible to use Optional.OfNullable().
bool valueIsSet = false;
int value = -1;
Optional<int> optional = valueIsSet 
    ? Optional.Of(value)
    : Optional<int>.EMPTY;
```
```csharp
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
// Functional methods to handle the presence or absence of an optional value.
optional.IfPresent(myValue => { ... });
optional.IfEmpty(() => { ... });
```
```csharp
// Operate on or transform an optional value only if it is present.
Optional<string> optStr = optional.Map(intValue => int.ToString());
```
```csharp
// Get default values or try alternatives if an optional is not present.
var newValue = optional.OrElse(() => 100);

var newValue = optional.OrTry(() => AnotherMethodThatReturnsOptional())
    .OrTry(() => ADifferentMethodThatReturnsOptional())
    .OrElseThrow(() => new InvalidOperationException("None of the optional values were present."));
```

### Result
The `Result` class allows a method to return a value or indicate failure without
throwing exceptions. This enforces that the error cases are handled, and allows
enumerations of the different error states.

```csharp
private Result<string, DatabaseException> ReadDatabaseEntry() {
    return Result.Success("Entry");
}

private void UseAResult() {
    var entry = ReadDatabaseEntry().OrDefault("No Entry");
}
```
The following helper methods are provided:
```csharp
// Create a result from a successful value or exception.
return Result.Success(value);
return Result.Failure(new Exception());
```
```csharp
// Check if the result is successful.
var result = GetAValue();
if (result.IsSuccess) {
    var value = (result as Success).Result;
}

if (result.IsFailure) {
    var error = (result as Failure).Error;
}
```
```csharp
// Check the result for a specific outcome.
if (result.Contains("Hello")) { ... }

if (result.ContainsError(new Exception()) { ... }
```
```csharp
// Operate on or transform a result if it is successful.
var stringResult = result.Map(value => value.ToString());

// Operate on or transform a result if it failed.
var newResult = result.MapError(ex => new ParseException(ex));
```
```csharp
// Get default values or try alternatives if a result failed.
var valueWithDefault = result.OrDefault("Default");
var valueWithAlternative = result.OrElse(() => "Alternative");
var requiredValue = result.OrThrow();
```

### Try.All
Additional utilities for trying multiple actions that may throw exceptions. If
any exceptions were thrown, they are aggregated into a single 
`AggregateException` that is thrown after all actions have been executed. This
is not an asynchronous operation, all actions are executed sequentially.
```csharp
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

IEnumerable<int> elements = GetElements();

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
// Constant string values.
public void FunctionWithDefault(string str = EmptyString) {
    // String.Empty is a readonly field and cannot be used as a default argument
    // or parameter in Attributes. EmptyString is a constant and can be used in
    // those cases.
}

public string GetStringValue() {
    return value?.ToString() ?? NullString;
}
```
```csharp
// Extension methods for converting objects to string.
object? obj = null;
string str = obj.ToStringSafe();

troubleSomeObject.ToDebugDisplayString();
```

---

<div align="center">
Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.<br/>
Licensed under the Apache License, Version 2.0.<br/>
See http://www.apache.org/licenses/LICENSE-2.0 for full license information.<br/>
</div>