using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions
{
    [Subject(typeof (Applicators), "WhenFailure")]
    internal class When_I_try_to_delete_a_file_and_register_for_failure
    {
        private static Action _deleteFile;
        private static string _testFile;
        private static bool _failureCallbackExecuted;

        private Establish context = () =>
        {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);
        };

        private Because of = () => Try.To(_deleteFile)
                                      .WhenFailure(error => _failureCallbackExecuted = true);

        private It should_not_execute_the_failure_callback = () => _failureCallbackExecuted.ShouldBeFalse();

        private Cleanup stuff = () => File.Delete(_testFile);
    }
}