# NiceTry

A (yet incomplete) port of the Try type of the Scala programming language () to the .NET platform, and some additions.

## Example

Reading the content of a url as string and printing it to the console. If the user specifies an invalid url, a fallback url will be used.
If an error occurs anywhere during the process, the error message will be printed to the console.

```csharp
Try.To(() => new Uri(Console.ReadLine()))
   .OrElse(FallbackUrl)
   .Map(url => {
   			using (var webClient = new WebClient()) {
   					return webClient.DownloadString(url);
            }
    })
    .Match(
    	content => Console.WriteLine("Success: {0}", content),
        error => Console.WriteLine("Failure: {0}", error.Message));
```