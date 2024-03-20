// -----------------------------------------------------------------------------
//  <copyright file="ReadmeTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Phx.Debug;
    using Phx.Dev;
    using Phx.Lang;
    using Phx.Test;
    using static Phx.Lang.Unit;
    using static Phx.Lang.StringUtils;

    /// <summary>
    /// For the most part, these don't actually test anything. They just need to make sure that the examples in the readme compile.
    /// </summary>
    public static class ReadmeTests {
        public class PhxDebugTests : LoggingTestClass {
            // -----------------------------------------------------------------
            // using System.Diagnostics;
            // using Phx.Debug;
            
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
            // -----------------------------------------------------------------
        }
        
        public class PhxDevTests {
            public void CompileTodo() {
                // -------------------------------------------------------------
                // using Phx.Dev;
                
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
                // -------------------------------------------------------------
            }
            
            // -------------------------------------------------------------
            // using Phx.Dev;
            
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
            // -------------------------------------------------------------
        }
        
        public class PhxLangTests {
            // -------------------------------------------------------------
            // using Phx.Lang;
            
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
            // -------------------------------------------------------------

            public void CompileLateInit() {
                // -------------------------------------------------------------
                // using Phx.Lang;
                
                string str = Late.Init<string>();
                // -------------------------------------------------------------
            }

            private string nickName = "Big D";
            // -------------------------------------------------------------
            // using Phx.Lang;

            private IOptional<string> GetNickName() {
                return Optional.OfNullable(nickName);
            }

            private void UseAnOptional() {
                var nickName = GetNickName().OrElse(() => StringUtils.EmptyString);
            }
            // -------------------------------------------------------------

            private string? ReadNickName() {
                return ToDo.NotImplementedYet<string?>("And never will be.");
            }

            private string ReadValue() {
                return ToDo.NotImplementedYet<string>("This won't either.");
            }
            
            private bool valueIsSet() {
                return ToDo.NotImplementedYet<bool>("This won't either.");
            }
            
            private IOptional<int> AnotherMethodThatReturnsOptional() {
                return ToDo.NotImplementedYet<IOptional<int>>("This won't either.");
            }
            private IOptional<int> ADifferentMethodThatReturnsOptional() {
                return ToDo.NotImplementedYet<IOptional<int>>("This won't either.");
            }
            
            public void CompileOptional() {
                // -------------------------------------------------------------
                // using Phx.Lang;
                
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
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;
                
                // Perform custom logic based on whether the optional is present
                if (optional.IsPresent) {
                    // Always check `IsPresent` first as `Value` will throw an exception if the 
                    // optional is empty.
                    var myValue = optional.Value;
                }

                if (optional.IsEmpty) {
                    return;
                }
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;

                // Functional methods to handle the presence or absence of an optional value.
                optional.IfPresent(myValue => { /* ... */ });
                optional.IfEmpty(() => { /* ... */ });
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;

                // Operate on or transform an optional value only if it is present.
                IOptional<string> optStr = optional.Map<int, string>(intValue => intValue.ToString());
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;

                // Get default values or try alternatives if an optional is not present.
                var v = optional.OrElse(() => 100);

                var v2 = optional.OrTry(() => AnotherMethodThatReturnsOptional())
                        .OrTry(() => ADifferentMethodThatReturnsOptional())
                        .OrThrow(() => new InvalidOperationException("None of the optional values were present."));
                // -------------------------------------------------------------
            }

            private class DatabaseException : Exception { }

            private class ParseException : Exception {
                public ParseException(Exception ex) { }
            }

            public IResult<string, Exception> GetAValue() {
                return ToDo.NotImplementedYet<IResult<string, Exception>>("Only here for compilation.");
            }
            
            // -------------------------------------------------------------
            // using Phx.Lang;
            
            private IResult<string, DatabaseException> ReadDatabaseEntry() {
                return Result.Success<string, DatabaseException>("Entry");
            }

            private void UseAResult() {
                var entry = ReadDatabaseEntry().OrElse(() => "No Entry");
            }
            // -------------------------------------------------------------

            public void CompileResult() {
                // -------------------------------------------------------------
                // using Phx.Lang;
                
                // Create a result from a successful value or exception.
                Result.Success(10);
                Result.Failure(new Exception());
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;
                
                // Check if the result is successful.
                var result = GetAValue();
                if (result.IsSuccess) {
                    var value = (result as Success<string, Exception>)!.Result;
                }

                if (result.IsFailure) {
                    var error = (result as Failure<string, Exception>)!.Error;
                }
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;

                // Check the result for a specific outcome.
                if (result.Contains(it => it == "Hello")) { /* ... */ }

                if (result.ContainsError(it => it is DatabaseException)) { /* ... */ }
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;

                // Operate on or transform a result if it is successful.
                var stringResult = result.Map(value => value.ToString());

                // Operate on or transform a result if it failed.
                var newResult = result.MapError(ex => new ParseException(ex));
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;

                // Get default values or try alternatives if a result failed.
                var valueWithAlternative = result.OrElse(() => "Alternative");
                var requiredValue = result.OrThrow();
                // -------------------------------------------------------------
            }

            private void DoFirstThing() { }
            private void DoSecondThing() { }
            private void DoThirdThing() { }
            private IEnumerable<int> GetElements() {
                return ToDo.NotImplementedYet<IEnumerable<int>>("Test Function"); 
            }
            
            private void DoSomethingWithEachItem(int element) { }
            private void log(Exception innerException) { }

            public void CompileTry() {
                // -------------------------------------------------------------
                // using Phx.Lang;
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
                // -------------------------------------------------------------
            }
            
            private bool PerformSomeSideEffect() {
                return ToDo.NotImplementedYet<bool>("Test Function"); 
            }
            
            // -------------------------------------------------------------
            // using Phx.Lang;
            // using static Phx.Lang.Unit;
            
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
            // -------------------------------------------------------------
            
            // -------------------------------------------------------------
            // using Phx.Lang;
            // using static Phx.Lang.StringUtils;
                
            // Constant string values.
            public void FunctionWithDefault(string str = EmptyString) {
                // String.Empty is a readonly field and cannot be used as a default argument
                // or parameter in Attributes. EmptyString is a constant and can be used in
                // those cases.
            }

            public string GetStringValue(string? value) {
                return value?.ToString() ?? NullString;
            }
            // -------------------------------------------------------------

            private object troublesomeObject = new object();
            private string inputValue = "Hello";
            
            public void CompileStringUtilsAndExtensions() {
                // -------------------------------------------------------------
                // using Phx.Lang;
                
                // Extension methods for converting objects to string.
                object? obj = null;
                string str = obj.ToStringSafe();

                troublesomeObject.ToDebugDisplayString();
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;
                
                // Utilities for inline string building.
                var newName = BuildString(sb => {
                            sb.Append("First");
                            sb.Append("Last");
                        });
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;
                
                // Extension methods for manipulating and escaping strings.
                var uppercase = "hello".StartUppercase();
                var lowercase = "Hello".StartLowercase();
                var implClassName = "IMyInterface".RemoveLeadingI();

                var verbatimString = "\"Hello\"".EscapeVerbatimString();
                var unescapedVerbatimString = verbatimString.UnescapeVerbatimString();

                var quoteString = "\"Hello\"".EscapeStringQuotes();
                var unescapedQuoteString = quoteString.UnescapeStringQuotes();
                // -------------------------------------------------------------
                
                // -------------------------------------------------------------
                // using Phx.Lang;
                
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
                // -------------------------------------------------------------
            }
        }
    }
}
