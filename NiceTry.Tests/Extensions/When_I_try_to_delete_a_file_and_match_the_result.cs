using System;
using System.IO;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_delete_a_file_and_match_the_result {
        static Action _deleteFile;
        static string _testFile;
        static bool _successCallbackExecuted;
        static Action _whenSuccess;
        static Action<Exception> _whenFailure;
        static Exception _error;

        Establish context = () => {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);

            _whenSuccess = () => _successCallbackExecuted = true;
            _whenFailure = error => _error = error;
        };

        Because of = () => Try.To(_deleteFile)
                              .Match(_whenSuccess, _whenFailure);

        It should_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeTrue();

        It should_not_execute_the_failure_callback = () => _error.ShouldBeNull();

        Cleanup stuff = () => File.Delete(_testFile);
    }
}