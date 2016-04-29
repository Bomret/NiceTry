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
The above example would evaluate `1 + 1` and - because that does not throw an exception - return a `Success<T>` and store it in the variable `result`. The result of the calculation is stored inside the `Success<T>` and can be accessed using the several methods, which are explined below:

```csharp
// using the Match method
result.Match(
    failure: err => /* an excepton was catched and is provided by err */,
    success: val => /* The operation successfully returned a value provided by val */);

// or using IfSuccess
result.IfSuccess(val => /* The operation successfully returned a value provided by val */);
```
To find out if `result` represents success or failure, it provides the boolean properties `IsSuccess` and `IsFailure`:

```csharp
if(result.IsSuccess)
    // do something;
```

### Creating a Try
There are several ways to create a `Try<T>`.

#### Try.To
Evaluates a `Func<T>` synchronously and returns a `Success<T>` if no exception is thrown or a `Failure<T>` otherwise.

```csharp
Try<int> @try = Try.To(() => 1 + 1);
```
#### Try.Success
Creates a `Try<T>` that represents success and wraps the specified value.

```csharp
Try<int> two = Try.Success(2);
```

#### Try.Failure
Creates a `Try<T>` that represents faliure and wraps the specified exception. Throws an `ArgumentNullException` when called with `null`.

```csharp
Try<int> failure = Try.Failure<int>(exception);
```

#### Try.Using
Properly creates, uses and disposes an `IDisposable` and creates a `Try<T>` that wraps the outcome of the operation.

```csharp
Try<string> content = Try.Using(
        () => File.OpenRead("story.txt"),
        stream => stream.ReadToEnd());
```

#### Using static imports in C# 6
C# 6 offers the feature to statically import classes and use the static methods therein without having to prefix them with the class name.
NiceTry provides a specific module for taking advantage of this feature.
```csharp
using static NiceTry.Predef

Try<int> two = Ok(2);

Try<int> err = Fail<int>(exception);

Try<int> two = Try(() => 1 + 1);

Try<string> two = Using(() => File.OpenRead("someFile.txt"), stream => stream.ReadToEnd());
```

### Accessing the wrapped value or exception
`Try<T>` implements a couple of methods to access the wrapped value or exception.

#### Match
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

#### IfSuccess
```csharp
Try.To(() => someFunc())
   .IfSuccess(val => /* do something with val */);
```
`IfSuccess` is only executed if `someFunc` does not throw an exception and delegates the produced value to its enclosed callback.

#### IfFailure
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



## Combinators
See the docs for a couple of extension methods that make working with `Try<T>` easier.