using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Retry), "To")]
    class When_I_retry_to_delete_a_file_with_an_invalid_path_up_to_two_times
    {
        static Action _deleteFileWithInvalidPath;
        static ITry _result;

        Establish context = () => _deleteFileWithInvalidPath = () => File.Delete(string.Empty);

        Because of = () => _result = Retry.To(_deleteFileWithInvalidPath);

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}