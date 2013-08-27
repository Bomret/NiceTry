using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions
{
    [Subject(typeof (NiceTry.Extensions), "WhenSuccess")]
    internal class When_I_try_to_delete_a_file_and_register_for_success
    {
        private static Action _deleteFile;
        private static string _testFile;
        private static bool _successCallbackExecuted;

        private Establish context = () =>
        {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);
        };

        private Because of = () => Try.To(_deleteFile)
                                      .WhenSuccess(() => _successCallbackExecuted = true);

        private It should_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeTrue();

        private Cleanup stuff = () => File.Delete(_testFile);
    }
}