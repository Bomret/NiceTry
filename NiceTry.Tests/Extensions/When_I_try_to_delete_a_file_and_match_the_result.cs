using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions))]
    internal class When_I_try_to_delete_a_file_and_match_the_result {
        private static Action _deleteFile;
        private static string _testFile;
        private static bool _successCallbackExecuted;
        private static Action _whenSuccess;
        private static Action<Exception> _whenFailure;
        private static Exception _error;

        private Establish context = () => {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);

            _whenSuccess = () => _successCallbackExecuted = true;
            _whenFailure = error => _error = error;
        };

        private Because of = () => Try.To(_deleteFile)
                                      .Match(_whenSuccess, _whenFailure);

        private It should_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeTrue();

        private It should_not_execute_the_failure_callback = () => _error.ShouldBeNull();

        private Cleanup stuff = () => File.Delete(_testFile);
    }
}