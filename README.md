# NiceTry
A functional wrapper type for the classic try/catch statement, inspired by its counterpart in the Scala programming language (http://www.scala-lang.org/).

Also available on NuGet (http://www.nuget.org/packages/NiceTry/).

Licensed under the Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0.html).
Uses the System.Reactive.Unit type made available by the guys working on the awesome Reactive Extensions (https://github.com/Reactive-Extensions) to represent the absence of a return value. 

## Example
Reading the content specified by a url as string and printing it to the console. If the user specifies an invalid url, a fallback url will be used.
If an error occurs anywhere during the process, the error message will be printed to the console.

```csharp
Try.To(() => Console.ReadLine())
    .Map(input => new Uri(input))
    .OrElse(new Uri("http://google.com"))
    .Using(() => new WebClient(),
           (wc, url) => wc.DownloadString(url))
    .Filter(content => string.IsNullOrWhiteSpace(content) == false)
    .Match(content => Console.WriteLine("Success: {0}", content),
           error => Console.WriteLine("Failure: {0}", error.Message));
```

## Retry
A buffed `Try` that retries the given work for up to the amount of given times:

```csharp
Retry.To(() => Console.ReadLine(), 3)
    .Map(input => new Uri(input))
    .OrElse(new Uri("http://google.com"))
    .Using(() => new WebClient(),
           (wc, url) => wc.DownloadString(url))
    .Filter(content => string.IsNullOrWhiteSpace(content) == false)
    .Match(content => Console.WriteLine("Success: {0}", content),
           error => Console.WriteLine("Failure: {0}", error.Message));
```

The above code sample would try reading a url from the command line up to 4 times (the original try and up to 3 retries). It returns a `Success` if the user specifies a valid url before all retries are used up. If the user does not specify a valid url the fourth time, the `Retry` will fail.

------

## Basics
`Try` is a wrapper around a simple try/catch statement. It tries to execute the given `Action` or `Func` and returns a `Success` if no exception was thrown and a `Failure` containing the encountered exception otherwise.

```csharp
Try<int> result = Try.To(() => 2 + 4);
```
The above example would evaluate the anonymous function `() => 2 + 4` and, since no exception will be thrown, return a `Success<int>` and store it in the variable `result`. The result of the calculation is stored inside the `Success` and can be accessed using the `Value` property:

```csharp
int six = result.Value;

// or

result.OnSuccess(i => _six = i);
```
To find out if `result` represents a `Success` or `Failure` it provides two boolean properties: `IsSuccess` and `IsFailure`. To be safe, you can either check those properties first or use the corresponding applicator.

If the above example would have thrown an exception, this could be accessed by the `Error` property or the `OnFailure` applicator:

```csharp
Exception error = result.Error;

// or

result.OnFailure(e => _error = e);
```

When you use `Try` or `Retry` to do an `Action` (which returns nothing) you'll get a `Try<Unit>` as result. The Unit type represents void (read more: http://en.wikipedia.org/wiki/Unit_type) and has been implemented to avoid the unnecessary duplication of combinator and applicator functions.

## Recommended usage
Every method that could throw an exception should return a `Try<T>` instead of `void` or the result directly. That way, eventual exceptions can be handled by applying combinators and applicators to the return value and exception handling can be managed hassle free.

So instead of this:
```csharp
private void DoSomethingRisky(string a) { //... };
private int CalculateSomethingRisky(int a, int b) { //... };
```

write this:
```csharp
private Try<Unit> DoSomethingRisky(string a) { //... };
private Try<int> CalculateSomethingRisky(int a, int b) { //... };
```

Using a `Try<T>` as return value states the risky nature of the method much clearer than a XML doc with an `<exception>` element.

## Creating a Try
There are several ways to create a `Try`.

### To
```csharp
Try<int> @try = Try.To(() => 2 + 3);
```
Executes an `Action` or a `Func<T>` synchronously and returns a `Success` if no exception was thrown or a `Failure` otherwise.

### Of / Success
```csharp
Try<int> five = Try.Of(5);

// same as

Try<int> five = Try.Success(5);
```
Create a `Success` that contains the given value.

### Failure
```csharp
Try<int> five = Try.Failure(new Exception());
```
Creates a `Failure` that contains the given Error.

### Using
```csharp
Try<string> result = Try.Using(() => new StreamReader(File.OpenRead("some.file")), reader => reader.ReadToEnd());
```
Simplifies working with disposables. The first function creates a disposable, the second one allows to use it.
In the example above, `result` would be a `Success<string>` containing the file content as text or a `Failure` if an error would have occurred.

### UsingWith
```csharp
var result = Try.Using(() => new StreamReader(File.OpenRead("some.file")),
                       reader => Try.To(() => reader.ReadToEnd()));
```
Simplifies working with disposables. The first function creates a disposable, the second one allows to use it.
In the example above, `result` would be a `Success<string>` containing the file content as text or a `Failure<string>` if an error would have occurred.

## Applicators and Combinators
As `Success` and `Failure` are simple data structures, a couple of extension methods are provided that make working with both types easier.

### Applicators
An applicator is either void or returns something different than a `Success` or `Failure`. Applicators can be used to execute actions with side effect. Applicators **do not** catch exceptions. You have to deal with those yourself.

#### OnSuccess
```csharp
Try.To(() => 2 + 3)
   .OnSuccess(i => _five = i);
```
`OnSuccess` is only executed if no exception was thrown and delegates the value of the `Success` to its enclosed callback. In the above example `_five` would be a int with Value `5`.

#### OnFailure
```csharp
Try.To(() => 5 / 0)
   .OnFailure(e => _error = e);
```
`OnFailure` is only executed if an exception was thrown and delegates the exception to its enclosed callback. In the above example `_error` would be a `DivideByZeroException`.

#### Match
```csharp
Try.To(() => 2 + 3)
   .Match(
   		i => _five = i,
   		e => _error = e);
```
`Match` allows to use a pattern matching like callback registration. The first function parameter is only executed in case of a `Success` and gets the value to work with. The second function parameter is registered to handle `Failure` and is only executed if an exception was thrown.

```csharp
string result = Try.To(() => 2 + 3)
                .Match(
   		             i => i.ToString(),
   		             e => "");
```

This overload for `Match` produces a value. In the above example `result` would be the string `"5"`.

#### Get
```csharp
int five = Try.To(() => 2 + 3).Get();
```
`Get` is the most straight forward applicator. It returns the value if the result is a `Success` or rethrows the exception, if one was encountered. In the above example `five` would be `5`.

#### GetOrElse
```csharp
int five = Try.To(() => 2 + 3).GetOrElse(-1);
```
`GetOrElse` either returns the value, if the `Try` returned a `Success` or the else value, if the `Try` returned a `Failure`. In the above example `five` would be `5`. It would have been `-1` if `() => 2 + 3` had thrown an exception.

#### GetOrDefault
```csharp
int five = Try.To(() => 2 + 3).GetOrDefault();
```
`GetOrDefault` either returns the value, if the `Try` returned a `Success` or the value of `default(T)`, if the `Try` returned a `Failure`. In the above example `five` would be `5`. It would have been `0` which is `default(int)` if `() => 2 + 3` had thrown an exception.

### Combinators
A combinator always returns a `Try` and thus lets you combine it with other combinators and allow function composition. 
This library provides a growing number of combinators that empowers you to write concise and bloat-free code for error handling. Some of them, like `Map` and `OrElse` have already been shown in the topmost example.

#### OrElse
```csharp
Try<int> result = Try.To(() => 5 / 0).OrElse(-1);
```
In the above examples a `DivideByZeroException` would be thrown and `result` would be a `Failure`. The `OrElse` combinator makes it possible to return a different value in case of a `Failure`. In both cases above `result` would be a `Success<int>` with the Value *-1*.

#### OrElseWith
```csharp
Try<int> result = Try.To(() => 5 / 0).OrElseWith(Try.Of(-1));
```
In the above example a `DivideByZeroException` would be thrown and `result` would be a `Failure`. The `OrElseWith` combinator makes it possible to return a different `Try` in case of a `Failure`. In both cases above `result` would be a `Success<int>` with the Value *-1*.

#### Then
```csharp
Try<Unit> result = Try.To(() => 2 + 3)
                      .Then(t => t.Get() + 1)
                      .Then(t => Print(t.Get()));
```
Allows chaining and conversion of multiple `Try's`. If a try fails, the chain will be interrupted and return immediately with a `Failure`.

#### ThenWith
```csharp
Try<Unit> result = Try.To(() => 2 + 3)
                      .ThenWith(t => t.Map(i => i + 1))
                      .ThenWith(t => t.Map(i => Print(i)));
```
Allows chaining and conversion of multiple `Try's`. If a try fails, the chain will be interrupted and return immediately with a `Failure`.

#### Apply
```csharp
Try<Unit> result = Try.To(() => 2 + 3)
				      .Apply(i => Print(i));
```
Applies an `Action<T>` to the value contained in a `Success<T>`. And returns the successful or failed result of this `Action`. In the above example `result` would be a `Success<Unit>` if `Print` would not throw an exception, otherwise a `Failure`.

#### Map
```csharp
string result = Try.To(() => 2 + 3)
				.Map(i => i.ToString());
```
`Map` allows to apply a function to the value of a `Success`. In the above example `result` would be a `Success<string>` with Value `"5"`.

#### FlatMap
```csharp
Try<string> result = Try.To(() => 2 + 3)
                        .FlatMap(i => Try.To(() => i.ToString()));
```
`FlatMap` allows to apply a function to the value of a `Success` that returns another `Try` and avoid the nesting that would occur otherwise. In the above example `result` would be a `Success<string>` with Value `"5"`. If `Map` would have been used, `result` would have been a `Success<Success<string>>`.

#### Filter
```csharp
Try<int> result = Try.To(() => 2 + 3)
				.Filter(i => i == 5);
```
`Filter` checks if a given predicate holds true for a `Try`. In the above example `result` would be a `Success<int>` with Value `5`. If the predicate `i => i == 5` would not hold, `result` would have been a `Failure` containing an `ArgumentException`. If `() => 2 + 3` would have thrown an exception `result` would have been a `Failure` containing the thrown exception.

#### Reject
```csharp
Try<int> result = Try.To(() => 2 + 3)
				     .Reject(i => i == 5);
```
Is the exact opposite of `Filter`. It checks if a given predicate does _not_ hold true for a `Try`. In the above example `result` would be a `Failure` containing an `ArgumentException`.

#### Zip
```csharp
Try<int> five = Try.To(() => 2 + 3);
Try<int> four = Try.To(() => 6 - 2);

Try<int> nine = five.Zip(four, (a, b) => a + b);
```
Takes one other `Try` and allows to apply a `Func` to the values of both `Try`'s. If one `Try` would be a `Failure` the function would not be applied.
In the above example `nine` would be a `Success<int>` containing `9`.

#### ZipWith
```csharp
Try<int> five = Try.To(() => 2 + 3);
Try<int> four = Try.To(() => 6 - 2);

Try<int> nine = five.ZipWith(four, (a, b) => Try.To(() => a + b));
```
Takes one other `Try` and allows to apply a `Func` to the values of both `Try`'s. If one `Try` would be a `Failure` the function would not be applied.
In the above example `nine` would be a `Success<int>` containing `9`.

#### Recover
```csharp
Try<int> result = Try.To(() => 5 / 0)
				     .Recover(e => -1);
```
This combinator is used to recover from a `Failure`. Gets the thrown exception to work with. In the above example `result` would be a `Success<int>` with Value `-1` and `e` would be a `DivideByZeroException`.

#### RecoverWith
```csharp
Try<int> result = Try.To(() => 5 / 0)
				     .RecoverWith(e => Try.Of(-1));
```
This combinator is used to recover from a `Failure` with a new `Try`. Gets the thrown exception to work with. In the above example `result` would be a `Success<int>` with Value `-1` and `e` would be a `DivideByZeroException`.

#### Transform
```csharp
Try<string> result = Try.To(() => 2 + 3)
        				.Transform(
        					i => i.ToString(),
        					e => e.Message);
```
Can be used to transform the result of a `Try`. The first function parameter transforms the resulting value if it is a `Success`, the second works on the exception if it is a `Failure`. In the above example `result` would be a `Success<string>` with Value *"5"*.

#### Succeed
```csharp
Try<int> result = Try.To(() => 2 + 3)
				     .Succeed(6);
```
Ignores the `Try` it is called from completely and returns a new `Success<T>` with a given value. In the above example `result` would be a `Success<int>` with value *6*.

#### Fail
```csharp
Try<int> result = Try.To(() => 2 + 3)
				     .Fail(new ArgumentException("I can't do this, Dave."));
```
Ignores the `Try` it is called from completely and returns a new `Failure` with a given Exception. In the above example `result` would be a `Failure` containing an Exception.

#### Retry
```csharp
Try<string> result = Try.To(() => 2 + 3)
				        .Retry(i => i.ToString(), 2);
```
Allows to retry a certain step a given number of times (at least once, if no retry count is given). Works the same as `Retry.To` as documented above.

#### Tap
```csharp
Try<int> result = Try.To(() => 2 + 3)
        			 .Tap(i => Console.WriteLine(i))
        			 .Map(i => i + 1);
```
Can be used to take a look at the value of a `Try` without modifying it. In the above example `result` would be a `Success<int>` containing the value *6*. `Tap` would have printed *5* to the console.

__Note:__ If the action in `Tap` throws, the exception is swallowed.

#### Finally
```csharp
Try<string> result = Try.To(() => File.ReadAllText("some.file"))
				        .Finally(() => File.Delete("some.file"))
				        .Map(content => content.Replace("meh", "awesome"));
```
Can be used to execute side effecting behavior without modifying a `Try`. In the above example `result` would be a `Success<string>` containing the read text. The given `Action` will be executed in _either case_, regardless if the incoming `Try` is a `Success` or a `Failure`.

__Note:__ If the action in `Finally` throws, the exception is swallowed.

#### Using
```csharp
Try<string> result = Try.Using(() => new StreamReader(File.OpenRead("some.file")), reader => reader.ReadToEnd());
                      
// or

Try<string> result = Try.To(() => File.OpenRead(file))
                        .Using(stream => new StreamReader(stream),
                               reader => reader.ReadToEnd())
```
Simplifies working with disposables. The first function creates a disposable, the second one allows to use it.
In both examples above, `result` would be a `Success<string>` containing the file content as text or a `Failure` if an error would have occurred.

#### UsingWith
```csharp
Try<string> result = Try.Using(() => new StreamReader(File.OpenRead("some.file")), reader => Try.To(() => reader.ReadToEnd()));
                      
// or

Try<string> result = Try.To(() => File.OpenRead(file))
                        .Using(stream => new StreamReader(stream),
                               reader => Try.To(() => reader.ReadToEnd()))
```
Simplifies working with disposables. The first function creates a disposable, the second one allows to use it.
In both examples above, `result` would be a `Success<string>` containing the file content as text or a `Failure` if an error would have occurred.