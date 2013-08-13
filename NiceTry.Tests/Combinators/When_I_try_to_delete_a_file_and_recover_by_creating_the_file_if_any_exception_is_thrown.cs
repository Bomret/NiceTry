using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Recover")]
    internal class When_I_try_to_delete_a_file_and_recover_by_creating_the_file_if_any_exception_is_thrown {
        private static ITry _result;
        private static Action _deleteFile;
        private static Action<Exception> _byCreatingFile;
        private static string _testFile;

        private Establish context = () => {
            _testFile = Path.GetTempFileName();

            _deleteFile = () => File.Delete(_testFile);

            _byCreatingFile = error => File.Create(_testFile);
        };

        private Because of = () => _result = Try.To(_deleteFile)
                                                .Recover(_byCreatingFile);

        private It should_delete_the_file = () => File.Exists(_testFile).ShouldBeFalse();

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        private Cleanup stuff = () => File.Delete(_testFile);
    }
}