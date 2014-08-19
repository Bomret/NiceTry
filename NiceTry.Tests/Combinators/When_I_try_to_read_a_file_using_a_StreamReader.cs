using System.IO;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Using")]
    public class When_I_try_to_read_a_file_using_a_StreamReader {
        static string _file;
        static string _content;
        static Try<string> _result;

        Establish context = () => {
            _file = Path.GetTempFileName();
            _content = "This is test content";

            File.WriteAllText(_file, _content);
        };

        Because of = () => _result = Try.To(() => File.OpenRead(_file))
                                        .Using(fileStream => new StreamReader(fileStream),
                                               reader => reader.ReadToEnd());

        It should_contain_the_content_of_the_file_in_the_success =
            () => _result.Value.Should().Be(_content);

        It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();

        Cleanup stuff = () => File.Delete(_file);
    }
}