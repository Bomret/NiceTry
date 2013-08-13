# NiceTry
A functional wrapper type for the classic try/catch statement, inspired by its counterpart in the Scala programming language (http://www.scala-lang.org/).

Also available on NuGet (http://www.nuget.org/packages/NiceTry/).

Licensed under the Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0.html).

## Example
Reading the content specified by a url as string and printing it to the console. If the user specifies an invalid url, a fallback url will be used.
If an error occurs anywhere during the process, the error message will be printed to the console.

```csharp
Try.To(() => new Uri(Console.ReadLine()))
   .OrElse(new Success<Uri>(FallbackUrl))
   .Map(url => {
   			using (var webClient = new WebClient()) {
   					return webClient.DownloadString(url);
            }
    })
    .Match(
    	content => Console.WriteLine("Success: {0}", content),
        error => Console.WriteLine("Failure: {0}", error.Message));
```

## Retry
A buffed `Try` that retries the given work for up to the amount of given times:

```csharp
Retry.To(() => new Uri(Console.ReadLine()), 3)
   .OrElse(new Success<Uri>(FallbackUrl))
   .Map(url => {
   			using (var webClient = new WebClient()) {
   					return webClient.DownloadString(url);
            }
    })
    .Match(
    	content => Console.WriteLine("Success: {0}", content),
        error => Console.WriteLine("Failure: {0}", error.Message));
```

The above code sample would try reading a url from the command line up to 4 times (the original try and up to 3 retries). It returns a `Success` if the user specifies a valid url before all retries are used up. If the user does not specify a valid url the fourth time, the `Retry` will fail.

------

## Basics
`Try` is a wrapper around a simple try/catch statement. It tries to execute the given `Action` or `Func` and returns a `Success` if no exception was thrown and a `Failure` containing the encountered exception otherwise.

```csharp
var result = Try.To(() => 2 + 4);
```

The above example would evaluate the anonymous function `() => 2 + 4` and, since no exception will be thrown, return a `Success<int>` and store it in the variable `result`. The result of the calculation is stored inside the `Success` and can be accessed using the `Value` property:

```csharp
var six = result.Value;
```

To find out if `result` represents a `Success` or `Failure` it provides two boolean properties: `IsSuccess` and `IsFailure`. To be safe, you can either check those properties first or use the corresponding extension:

```csharp
result.WhenSuccess(i => _six = i);
```

If the above example would have thrown an exception, it could be accessed by the `Error` property or the `WhenFailure` extension:

```csharp
var error = result.Error;

// or

result.WhenFailure(e => _error = e);
```

## Recommended usage
Every method that could throw an exception should return either an `ITry` or an `ITry<T>` instead of `void` or the result directly. That way, eventual exceptions can be handled by applying combinators and extensions to the return value and exception handling can be managed hassle free.

So instead of this:

```csharp
private void DoSomethingRisky(string a) { //... };
private int CalculateSomethingRisky(int a, int b) { //... };
```

write this:

```csharp
private ITry DoSomethingRisky(string a) { //... };
private ITry<int> CalculateSomethingRisky(int a, int b) { //... };
```

Using an `ITry` or an `ITry<T>` as return value states the risky nature of the method much clearer than a XML doc with an `<exception>` element.

## Extensions and Combinators
Since `Success` and `Failure` are simple data structures, a couple of extension methods are provided that make working with both types easier.

### Extensions
An extension is either void or returns something different than a `Success` or `Failure`. Extensions can be used to execute actions with side effect. Extensions **do not** catch exceptions. You have to deal with those yourself.

#### WhenComplete
```csharp
Try.To(() => 2 + 3)
   .WhenComplete(t => _result = t);
```

`WhenComplete` is always executed and delegates the result of the `Try` to its enclosed callback. In the above example `_result` would be a `Success<int>` with Value *5*.

#### WhenSuccess
```csharp
Try.To(() => 2 + 3)
   .WhenSuccess(i => _five = i);
```

`WhenSuccess` is only executed if no exception was thrown and delegates the value of the `Success` to its enclosed callback. In the above example `_five` would be a int with Value *5*.

#### WhenFailure
```csharp
Try.To(() => 5 / 0)
   .WhenFailure(e => _error = e);
```

`WhenFailure` is only executed if an exception was thrown and delegates the exception to its enclosed callback. In the above example `_error` would be a `DivideByZeroException`.

#### Match
```csharp
Try.To(() => 2 + 3)
   .Match(
   		i => _five = i,
   		e => _error = e);
```

`Match` allows to use a pattern matching like callback registration. The first function parameter is only executed in case of a `Success` and gets the value to work with. The second function parameter is registered to handle `Failure` and is only executed if an exception was thrown.

#### Get
```csharp
var five = Try.To(() => 2 + 3).Get();
```

`Get` is the most straight forward extension. It returns the value if the result is a `Success` or rethrows the exception, if one was encountered. In the above example `five` would be *5*.

#### GetOrElse
```csharp
var five = Try.To(() => 2 + 3).GetOrElse(-1);
```

`GetOrElse` either returns the value, if the `Try` returned a `Success` or the else value, if the `Try` returned a `Failure`. In the above example `five` would be *5*. It would have been *-1* if `() => 2 + 3` had thrown an exception.

### Combinators
A combinator always returns a `Success` or `Failure` and thus lets you combine it with other combinators and allows function composition. 
This library provides a growing number of combinators that empowers you to write concise and bloat-free code for error handling. Some of them, like `Map` and `OrElse` have already been shown in the topmost example.

#### OrElse
```csharp
var result = Try.To(() => 5 / 0)
				.OrElse(new Success<int>(-1));

// or

var result = Try.To(() => 5 / 0)
				.OrElse(() => new Success<int>(-1));
```

In the above examples a `DivideByZeroException` would be thrown and `result` would be a `Failure`. The 
`OrElse` combinator makes it possible to return a different `Try` in case of a `Failure`. In both cases above `result` would be a `Success<int>` with the Value *-1*.

#### AndThen
```csharp
var result = Try.To(() => 2 + 3)
                .AndThen(AddOne)
                .AndThen(PrintResult);
```

Allows chaining and conversion of multiple `Try's`. If a try fails, the chain will be interrupted and return immediately with a `Failure`. In the above example `result` would be a `Success`. Here are the signatures of the methods `AddOne` and `PrintResult`:

```csharp
ITry<int> AddOne(ITry<int> arg)
ITry PrintResult<T>(ITry<T> value)
```

#### Map
```csharp
var result = Try.To(() => 2 + 3)
				.Map(i => i.ToString());
```

`Map` allows to apply a function to the value of a `Success`. In the above example `result` would be a `Success<string>` with Value *"5"*.

#### Filter
```csharp
var result = Try.To(() => 2 + 3)
				.Filter(i => i == 5);
```

`Filter` checks if a given predicate holds true for a `Try`. In the above example `result` would be a `Success<int>` with Value *5*. If the predicate `i => i == 5` would not hold, `result` would have been a `Failure<int>` containing an `ArgumentException`. If `() => 2 + 3` would have thrown an exception `result` would have been the original `Failure<int>`.

#### FlatMap
```csharp
var result = Try.To(() => 2 + 3)
                .FlatMap(i => Try.To(() => i.ToString()));
```

`FlatMap` allows to apply a function to the value of a `Success` that returns another `Try` and avoid the nesting that would occur otherwise. In the above example `result` would be a `Success<string>` with Value *"5"*. If `Map` would have been used, `result` would have been a `Success<Success<string>>`.

#### Recover
```csharp
var result = Try.To(() => 5 / 0)
				.Recover(e => -1);
```

This combinator is used to recover from a `Failure`. In the above example `result` would be a `Success<int>` with Value *-1*.

#### Transform
```csharp
var result = Try.To(() => 2 + 3))
				.Transform(
					i => i.ToString(),
					e => e.Message);
```

Can be used to transform the result of a `Try`. The first function parameter transforms the resulting value if it is a `Success`, the second works on the exception if it is a `Failure`. In the above example `result` would be a `Success<string>` with Value *"5"*.

## Attributions
**Icon:** <a href="http://thenounproject.com/noun/approve/#icon-No330" target="_blank">Approve</a>  from The Noun Project