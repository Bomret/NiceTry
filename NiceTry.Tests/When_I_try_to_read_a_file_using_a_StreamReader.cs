using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "Using")]
    public class When_I_try_to_read_a_file_using_a_StreamReader {
        static string _file;
        static string _content;
        static ITry<string> _result;

        Establish context = () => {
            _file = Path.GetTempFileName();
            _content = "This is test content";

            File.WriteAllText(_file, _content);
        };

        Because of = () => _result = Try.Using(() => new StreamReader(File.OpenRead(_file)),
                                               reader => reader.ReadToEnd());

        It should_contain_the_content_of_the_file_in_the_success =
            () => _result.Value.ShouldEqual(_content);

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        Cleanup stuff = () => File.Delete(_file);
    }
}