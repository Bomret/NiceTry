using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try))]
    public class When_I_try_to_delete_a_file {
        static ITry _result;
        static string _testFile;
        static Action _deleteFile;

        Establish context = () => {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);
        };

        Because of = () => _result = Try.To(_deleteFile);

        It should_delete_the_file = () => File.Exists(_testFile).ShouldBeFalse();

        It should_not_contain_an_exception = () => _result.Error.ShouldBeNull();

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        Cleanup stuff = () => File.Delete(_testFile);
    }
}