using System;
using System.IO;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Combinators))]
    internal class When_I_try_to_delete_a_file_and_recover_by_creating_the_file_if_any_exception_is_thrown {
        static ITry _result;
        static Action _deleteFile;
        static Action<Exception> _byCreatingFile;
        static string _testFile;

        Establish context = () => {
            _testFile = Path.GetTempFileName();

            _deleteFile = () => File.Delete(_testFile);

            _byCreatingFile = error => File.Create(_testFile);
        };

        Because of = () => _result = Try.To(_deleteFile)
                                        .Recover(_byCreatingFile);

        It should_delete_the_file = () => File.Exists(_testFile).ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        Cleanup stuff = () => File.Delete(_testFile);
    }
}