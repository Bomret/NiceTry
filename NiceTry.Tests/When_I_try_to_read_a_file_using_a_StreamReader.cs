using System.IO;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Try), "Using")]
    public class When_I_try_to_read_a_file_using_a_StreamReader
    {
        private static string _file;
        private static string _content;
        private static Try<string> _result;

        private Establish context = () =>
        {
            _file = Path.GetTempFileName();
            _content = "This is test content";

            File.WriteAllText(_file, _content);
        };

        private Because of = () => _result = Try.Using(() => new StreamReader(File.OpenRead(_file)),
            reader => reader.ReadToEnd());

        private It should_contain_the_content_of_the_file_in_the_success =
            () => _result.Value.Should().Be(_content);

        private It should_not_return_a_failure = () => _result.IsFailure.Should().BeFalse();

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();

        private Cleanup stuff = () => File.Delete(_file);
    }
}