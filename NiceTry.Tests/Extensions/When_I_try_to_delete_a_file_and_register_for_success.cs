using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Applicators), "IfSuccess")]
    internal class When_I_try_to_delete_a_file_and_register_for_success {
        static Action _deleteFile;
        static string _testFile;
        static bool _successCallbackExecuted;

        Establish context = () => {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);
        };

        Because of = () => Try.To(_deleteFile)
                              .WhenSuccess(() => _successCallbackExecuted = true);

        It should_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeTrue();

        Cleanup stuff = () => File.Delete(_testFile);
    }
}