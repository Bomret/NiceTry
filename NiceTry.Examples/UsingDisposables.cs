using System;
using System.IO;
using System.Net;
using NiceTry.Combinators;

namespace NiceTry.Examples {
    public class UsingDisposables {
        public void ReadFileWithUsingAsFirstStatement() {
            const string file = "some.file";

            Try.Using(() => new StreamReader(File.OpenRead(file)),
                      reader => reader.ReadToEnd())
               .OnSuccess(Console.WriteLine);
        }

        public void ReadFileWithUsingAsInBetweenStatement() {
            const string file = "some.file";

            Try.To(() => File.OpenRead(file))
               .Using(stream => new StreamReader(stream),
                      reader => reader.ReadToEnd())
               .OnSuccess(Console.WriteLine);
        }

        public void DownloadFromUrlWithUsingAsInBetweenStatement() {
            Try.To(() => new Uri("http://google.com"))
               .Using(() => new WebClient(),
                      (wc, url) => wc.DownloadString(url))
               .OnSuccess(Console.WriteLine);
        }

        public void ReadFileUploadContentToUrlFinallyDeleteFile() {
            const string file = "some.file";
            var url = new Uri("http://some.url");

            Try.To(() => File.OpenRead(file))
               .Using(stream => new StreamReader(stream),
                      reader => reader.ReadToEnd())
               .Using(() => new WebClient(),
                      (wc, text) => wc.UploadString(url, text))
               .Tap(_ => File.Delete(file))
               .Match(text => Console.WriteLine("Uploaded {0} to {1}", text, url),
                      error => Console.WriteLine("Encountered exception: {0}", error.Message));
        }
    }
}