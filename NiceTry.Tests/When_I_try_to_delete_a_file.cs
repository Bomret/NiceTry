using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "To")]
    public class When_I_try_to_delete_a_file {
        private static ITry _result;
        private static string _testFile;
        private static Action _deleteFile;

        private Establish context = () => {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);
        };

        private Because of = () => _result = Try.To(_deleteFile);

        private It should_delete_the_file = () => File.Exists(_testFile).ShouldBeFalse();

        private It should_not_contain_an_exception = () => _result.Error.ShouldBeNull();

        private It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        private Cleanup stuff = () => File.Delete(_testFile);
    }
}