# NiceTry
A type for the classical try/catch statement that allows functional and bloat free error handling. Inspired by the Try type in Scala. Licensed under the [MPL-2.0 License](https://opensource.org/licenses/MPL-2.0).

[![NuGet Status](http://img.shields.io/nuget/v/NiceTry.svg)](https://www.nuget.org/packages/NiceTry/)
[![Issue Stats](http://www.issuestats.com/github/bomret/nicetry/badge/pr?style=flat)](http://www.issuestats.com/github/bomret/nicetry)
[![Issue Stats](http://www.issuestats.com/github/bomret/nicetry/badge/issue?style=flat)](http://www.issuestats.com/github/bomret/nicetry)
[![Stories in Ready](https://badge.waffle.io/Bomret/NiceTry.svg?label=ready&title=Ready)](http://waffle.io/Bomret/NiceTry)

## Build status
|  |  Status of last build |
| :------ | :------: |
| **Mono** | [![Travis build status](https://img.shields.io/travis/Bomret/NiceTry.svg)](https://travis-ci.org/Bomret/NiceTry) |
| **Windows** | [![AppVeyor Build status](https://img.shields.io/appveyor/ci/stefanreichel/nicetry.svg)](https://ci.appveyor.com/project/StefanReichel/nicetry) |

## Example
Reading the content type of a url as string and printing it to the console. If any of the lambdas throws an exception the calls to `Select` and `Match` would not execute and *"An error occured: {error details}."* would be printed to the console.

```csharp
Try.To(() => WebRequest.Create(Url) as HttpWebRequest)
    .Select(request => request.GetResponse()?.ContentType)
    .Match(
        success: contentType => Console.WriteLine($"Content-Type: {contentType}"),
        failure: err => Console.WriteLine($"An error occured: {err}."));
```

The same can be written using LINQ syntax:

```csharp
var maybeText =
    from req in Try.To(() => WebRequest.Create(Url) as HttpWebRequest)
    let contentType = req.GetResponse()?.ContentType
    select contentType;

maybeText.Match(
    success: contentType => Console.WriteLine($"Content-Type: {contentType}"),
    failure: err => Console.WriteLine($"An error occured: {err}."));
```

------

## License
The [Mozilla Public License Version 2.0](https://opensource.org/licenses/MPL-2.0)

## Troubleshooting and support
Did you find a bug or have an idea for a new feature? Open a new [Issue](https://github.com/Bomret/NiceTry/issues).

## Contributing
* Read the [Code of Conduct](https://github.com/Bomret/NiceTry/blob/master/CODE_OF_CONDUCT.md)
* Take a look at the [Issues](https://github.com/Bomret/NiceTry/issues). If there is one you want to work on (has labels `ready` and `up-for-grabs`), write a comment that you want to work on it. If you have a new idea/problem, please open an issue and explain it.
* Fork the repo and clone it on your machine.
* Build the project running `build.cmd` on Windows or `build.sh` on Mac OSX/Linux.
* Write your code and don't forget to add xml docs and tests.
* Run the `build.*` for your platform again to ensure the build works and all tests pass.
* Describe your feature in the README, if applicable (e.g. new combinator).
* Create a pull request.

## Versioning
This project uses [SemVer](http://semver.org/) compatible versioning, which means `Breaking.Feature.Fix`.
* `Breaking`: Changes that break API compatibility with earlier versions.
* `Feature`: Added functionality that don't break API compatibility with earlier versions.
* `Fix`: Backwards compatible bugfixes and refactoring/clean up.

## Deprecation of features
If a feature becomes deprecated it is marked with the `Obsolete` attribute and will not throw a compiler error.
In the next release it will throw a compiler error and the major version is incremented by 1 because of breaking changes.
In the release after that, the feature will be removed and its patch version is incremented by 1.

### Example
* 3.1.0.6
```csharp
[Obsolete("This method is deprecated and will be removed in 2 releases.")]
public bool TryGet(out value) => // ...
```

* 4.0.0
```csharp
[Obsolete("This method is deprecated and will be removed in the next release.", true)]
public bool TryGet(out value) => // ...
```

* 4.0.1
```csharp
// TryGet removed
```

## Maintainers
* [Stefan Reichel (@bomret)](https://github.com/Bomret)

------

## Basics
`Try<T>` represents the successful or failed outcome of an operation. It might contain a value that was produced by said operation.

```csharp
Try<int> result = Try.To(() => 1 + 1);
```
The above example would evaluate `1 + 1` and - because that does not throw an exception - return a `Success<T>` and store it in the variable `result`. The result of the calculation is stored inside the `Success<T>` and can be accessed using the `Match`, `IfSuccess` or several `Get*` methods:

```csharp
// using the Match method
result.Match(
    failure: err => /* an excepton was catched and is provided by err */,
    success: val => /* The operation successfully returned a value provided by val */);

// or using IfSuccess
result.IfSuccess(val => /* The operation successfully returned a value provided by val */);
```
To find out if `result` represents success or failure it provides the boolean properties `IsSuccess` and `IsFailure`:

```csharp
if(result.IsSuccess)
    // do something;
```

### Match
```csharp
Try.To(() => 1 + 1).Match(
    failure: err => { /* an excepton was catched and is provided by err */ },
    success: val => /* The operation successfully returned a value provided by val */);
```
`Match` allows to use a pattern matching like callback registration. Only the appropriate function parameter for success or failure is executed and gets the value or exception to work with.

```csharp
string result = Try.To(() => 1 + 1).Match(
    failure: err => err.ToString(),
   	success: val => val.ToString());
```
This overload for `Match` produces a value. In the above example `result` would be the string `"2"`.

### IfSuccess
```csharp
Try.To(() => someFunc())
   .IfSuccess(value => /* do something with the value */);
```
`IfSuccess` is only executed if `someFunc` does not throw an exception and delegates the produced value to its enclosed callback.

### IfFailure
```csharp
Try.To(() => someFunc())
   .IfFailure(err => /* react to the error */);
```
`IfFailure` is only executed if `someFunc` throws an exception and delegates the catched exception to its enclosed callback.

## Recommended usage
Every method that may throw an exception in some circumstances should return a `Try<T>` instead of the result directly. That way, eventual unexpected exceptions can be avoided. It is adviseable to not catch exceptions that signal developer errors, like ArgumentException or ArgumentNullException for example.

So instead of this:
```csharp
string DoSomethingThatCouldThrow(string arg) { /* ... */ }
```

write this:
```csharp
Try<string> DoSomethingThatCouldThrow(string arg) { /* ... */ }
```

## Creating a Try
There are several ways to create a `Try<T>`.

### Try.To
```csharp
Try<int> @try = Try.To(() => 1 + 1);
```
Evaluates a `Func<T>` synchronously and returns a `Success<T>` if no exception is thrown or a `Failure<T>` otherwise.

### FromTryPattern
```csharp
Option<double> option = Option.FromTryPattern<string, double>(Double.TryParse, "2.6");
```
Evaluates the call to a given method that follows the TryParse pattern and arguments synchronously and returns a `Some` if the method succeeded or `None` otherwise.

Currently the method is overloaded with versions that take up to 16 args.

### None
```csharp
Option<int> none = Option.None;
// same as
Option<int> none = Option<int>.None;
```
Returns `None` that represents the absence of a value.

### ToOption
```csharp
Option<int> none = 3.ToOption();
```
Converts any value to an `Option<T>` by calling `Option.From` on it.

### Using static imports in C# 6
C# 6 offers the feature to statically import classes and use the static methods therein without having to prefix them with the class name.
NiceTry provides a specific module for taking advantage of this feature.
```csharp
using static NiceTry.Predef

Option<int> two = Option(2);
// or
Option<int> two = Some(2);
// io for None
Option<int> none = None;
```

## Combinators
Since `Some` and `None` are simple data structures, a couple of extension methods are provided that make working with both types easier.

### Contains
```csharp
bool containsFive = Option.From(5).Contains(5);
```
Returns `true` if the `Option` it is applied to is a `Some` containing the desired value otherwise `false`.

### Normalize
```csharp
Option<int?> option = Option.From<int?>(5);
Option<int> normalized = option.Normalize();
```
Normalizes an `Option<T?>` into its `Option<T>` representation.

### ToNullable
```csharp
Option<DateTime> nowOption = Option.From(DateTime.Now)

DateTime? maybeNow = nowOption.ToNullable();
```
Converts an `Option<T>` (where `T` is a value type) to a `Nullable<T>`.

### Get
```csharp
int two = Option.From(2).Get();
```
`Get` is the most straight forward extension. It returns the value if the result is a `Some` or throws an `InvalidOperationException`, if `None`. In the above example `two` would be `2`.

### GetOrElse
```csharp
int two = Option.From(2).GetOrElse(-1);
// or lazily with a func
int two = Option.From(2).GetOrElse(() => -1);
```
`GetOrElse` either returns the value, if `From` returned a `Some` or the else value, if `From` returned `None`. In the above example `two` would be `2`. It would have been `-1` if `2` was `null`.

### GetOrDefault (Deprecated)
```csharp
int two = Option.From(2).GetOrDefault();
```
`GetOrDefault` either returns the value, if `From` returned a `Some` or the value of `default(T)`, if `From` returned `None`. In the above example `two` would be `2`. It would have been `0` which is `default(int)` if `2` was `null`.

### OrElse
```csharp
Option<int> result = Option.From<int?>(null).OrElse(-1);
// or lazily with a func
Option<int> result = Option.From<int?>(null).OrElse(() => -1);
```
In the above examples `null` would have been returned and `result` would be `None`. The `OrElse` combinator makes it possible to return a different value in case of `None`. `result` would be a `Some<int>` with the Value `-1`.

### OrElseWith
```csharp
Option<int> result = Option.From<int?>(null).OrElseWith(Option.From(-1));
// or lazily with a func
Option<int> result = Option.From<int?>(null).OrElseWith(() => Option.From(-1));
```
In the above examples `null` would have been returned and `result` would be `None`. The `OrElseWith` combinator makes it possible to return a different `Option` in case of `None`. `result` would be a `Some<int>` with the Value `-1`.

### Select
```csharp
Option<string> result = Option.From(2).Select(i => i.ToString());
```
`Select` allows to apply a function to the value of a `Some`. In the above example `result` would contain the string value `"5"`.

### SelectMany
```csharp
Option<string> result = Option.From(2).SelectMany(i => Option.From(i.ToString()));
```
`SelectMany` allows to apply a function to the value of a `Some` that returns another `Option` and avoid the nesting that would occur otherwise. In the above example `result` would be a `Some` with Value `"5"`. If `Some` would have been used, `result` would have been a `Option<Option<string>>`.

### Zip
```csharp
Option<int> five = 5;
Option<int> four = 4;

Option<int> nine = five.Zip(four, (a, b) => a + b);
```
Takes another `Option` and allows to apply a `Func` to the values of both options. If one `Option` would be `None` the function would not be applied.
In the above example `nine` would be a `Some` containing `9`.

### ZipWith
```csharp
Option<int> five = 5;
Option<int> four = 4;

Option<int> nine = five.ZipWith(four, (a, b) => Option.From(a + b));
```
Takes another `Option` and allows to apply a `Func` to the values of both options. If one `Option` would be `None` the function would not be applied.
In the above example `nine` would be a `Some` containing `9`.

### Transform
```csharp
Option<string> result = Option.From(2).Transform(
    Some: i => i.ToString(),
    None: () => "");
```
Can be used to transform the value of an `Option`. The first function parameter transforms the resulting value if it is a `Some`, the second returns a value if it is `None`. In the above example `result` would be a `Some` with value `"5"`.

### Do
```csharp
Option<string> result = Option.From(2)
    .Do(i => Console.Write($"Do executed on {i}"))
    .Select(i => i.ToString());
```
Can be used to execute side effecting behavior without modifying an `Option`. In the above example `result` would be a `Some` containing `"2"`. The given `Action` will be executed in _either case_, regardless if the incoming `Option` is a `Some` or `None`.

### Flatten
```csharp
Option<Option<int>> nestedOption = Option.From(Option.From(2));
Option<int> result = nestedOption.Flatten();
```
Flattens a nested `Option`.

### Switch
```csharp
Option<int?> two = Option.From(2);
Option<int?> nil = Option.From<int?>(null);
Option<int?> three = Option.From(3);

Option<string> result = nil.Switch(three, two);
```
Returns the first `Option` that contains a value or `None` if all are `None`.

## Combinators for `IEnumerable<T>`
NiceTry contains several extensions that integrate `Option<T>` with `IEnumerable<T>`.

### Exchange
```csharp
IEnumerable<int?> ints = new [] { 1, 2, default(int?), 4 };
IEnumerable<Option<int>> optionalInts = Option.From(ints).Exchange();
```
If the given `Option<IEnumerable<T>>` is a `Some`, `Option.From` is applied to all values of the enumerable. If it is `None` instead, an empty enumerable is returned.

### SelectValues
```csharp
IEnumerable<Option<int?>> ints = new[] {1, 2, default(int?), 4};
IEnumerable<int> values = ints.SelectValues();
```
Select only the values contained in the options of the given enumerable. If it only contains `None`, an empty enumerable is returned.

### AggregateOptional
```csharp
IEnumerable<string> strings = new[] {"a", "b", default(string), "d"};
Option<string> abd = strings.AggregateOptional((a, c) => a + c);
```
Like the standard Linq `Aggregate` but automatically handles null elements and results in an option.

### AggregateOptionalNullable
```csharp
IEnumerable<int?> ints = new[] {1, 2, default(int?), 4};
Option<int> seven = ints.AggregateOptional((a, c) => a + c);
```
Like `AggregateOptional` but for `Nullable<T>` elements.

### AllOrNone
```csharp
IEnumerable<Option<string>> strings = new[] {"a", "b", default(string), "d"};
Option<IEnumerable<string>> abd = strings.AllOrNone();
```
Returns an option containing all values or `None`, if any of the options in the enumerable does not contain a value or the enumerable is empty.

### FirstOptional
```csharp
IEnumerable<string> strings = new[] {"a", "b", default(string), "d"};
Option<string> a = strings.FirstOptional();
```
Returns a `Some` containing the first value if the enumerable contains at least one value, else `None`.

### LastOptional
```csharp
IEnumerable<string> strings = new[] {"a", "b", default(string), "d"};
Option<string> d = strings.LastOptional();
```
Returns a `Some` containing the last value if the enumerable contains at least one value, else `None`.

### SingleOptional
```csharp
IEnumerable<string> strings = new[] {"a"};
Option<string> a = strings.SingleOptional();
```
Like `Single` but returns the only element in the enumerable wrapped in an option. If this enumerable is empty or the single element is NULL, None is returned. Throws an exception if this enumerable contains more than one element.

### SingleOptionalNullable
```csharp
IEnumerable<string> strings = new[] {"a"};
Option<string> a = strings.SingleOptional();
```
Like `SingleOptional` but for `Nullable<T>` elements.