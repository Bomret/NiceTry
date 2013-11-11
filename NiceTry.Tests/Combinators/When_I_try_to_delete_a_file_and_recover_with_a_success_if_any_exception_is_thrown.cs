using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "RecoverWith")]
    internal class When_I_try_to_delete_a_file_and_recover_with_a_success_if_any_exception_is_thrown
    {
        private static ITry _result;
        private static Action _deleteFile;
        private static string _testFile;
        private static Func<Exception, ITry> _success;

        private Establish context = () =>
        {
            _testFile = Path.GetTempFileName();

            _deleteFile = () => File.Delete(_testFile);

            _success = error => new Success();
        };

        private Because of = () => _result = Try.To(_deleteFile)
                                                .RecoverWith(_success);

        private It should_delete_the_file = () => File.Exists(_testFile).ShouldBeFalse();

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        private Cleanup stuff = () => File.Delete(_testFile);
    }
}