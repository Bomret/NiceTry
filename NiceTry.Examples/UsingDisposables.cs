using System;
using System.IO;
using System.Net;

namespace NiceTry.Examples {
    public class UsingDisposables {
        public void ReadFileWithUsingAsFirstStatement() {
            const string file = "some.file";

            Try.Using(() => new StreamReader(File.OpenRead(file)),
                      reader => reader.ReadToEnd())
               .WhenSuccess(fileContent => Console.WriteLine(fileContent));
        }

        public void ReadFileWithUsingAsInBetweenStatement() {
            const string file = "some.file";

            Try.To(() => File.OpenRead(file))
               .Using(stream => new StreamReader(stream),
                      reader => reader.ReadToEnd())
               .WhenSuccess(fileContent => Console.WriteLine(fileContent));
        }

        public void DownloadFromUrlWithUsingAsInBetweenStatement() {
            Try.To(() => new Uri("http://google.com"))
               .Using(() => new WebClient(),
                      (wc, url) => wc.DownloadString(url))
               .WhenSuccess(content => Console.WriteLine(content));
        }

        public void ReadFileUploadContentToUrlFinallyDeleteFile() {
            const string file = "some.file";
            var url = new Uri("http://some.url");

            Try.To(() => File.OpenRead(file))
               .Using(stream => new StreamReader(stream),
                      reader => reader.ReadToEnd())
               .Using(() => new WebClient(),
                      (wc, text) => wc.UploadString(url, text))
               .Do(() => File.Delete(file))
               .Match(text => Console.WriteLine("Uploaded {0} to {1}", text, url),
                      error => Console.WriteLine("Encountered exception: {0}", error.Message));
        }
    }
}