using System;
using System.Net;

namespace NiceTry.Examples {
    class Generic {
        public void ReadUrlFromConsoleDownloadContentAndPrint() {
            Try.To(() => Console.ReadLine())
               .Map(input => new Uri(input))
               .OrElse(new Uri("http://google.com"))
               .Using(() => new WebClient(), (wc, url) => wc.DownloadString(url))
               .Filter(content => string.IsNullOrWhiteSpace(content) == false)
               .Match(content => Console.WriteLine("Success: {0}", content),
                      error => Console.WriteLine("Failure: {0}", error.Message));
        }
    }
}