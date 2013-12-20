using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "WhenFailure")]
    internal class When_I_try_to_delete_a_file_and_register_for_failure {
        static Action _deleteFile;
        static string _testFile;
        static bool _failureCallbackExecuted;

        Establish context = () => {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);
        };

        Because of = () => Try.To(_deleteFile)
                              .WhenFailure(error => _failureCallbackExecuted = true);

        It should_not_execute_the_failure_callback = () => _failureCallbackExecuted.ShouldBeFalse();

        Cleanup stuff = () => File.Delete(_testFile);
    }
}