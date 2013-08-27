using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Retry), "To")]
    internal class When_I_retry_to_delete_a_file_with_an_invalid_path_up_to_two_times
    {
        private static Action _deleteFileWithInvalidPath;
        private static ITry _result;

        private Establish context = () => _deleteFileWithInvalidPath = () => File.Delete(string.Empty);

        private Because of = () => _result = Retry.To(_deleteFileWithInvalidPath);

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}