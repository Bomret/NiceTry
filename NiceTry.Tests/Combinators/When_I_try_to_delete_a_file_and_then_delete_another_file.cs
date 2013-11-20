using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Then")]
    internal class When_I_try_to_delete_a_file_and_then_delete_another_file {
        static Action _deleteFileOne;
        static Func<ITry, ITry> _deleteFileTwo;
        static ITry _result;
        static string _testFileOne;
        static string _testFileTwo;

        Establish context = () => {
            _testFileOne = Path.GetTempFileName();
            _testFileTwo = Path.GetTempFileName();

            _deleteFileOne = () => File.Delete(_testFileOne);
            _deleteFileTwo = t => Try.To(() => File.Delete(_testFileTwo));
        };

        Because of = () => _result = Try.To(_deleteFileOne)
            .Then(_deleteFileTwo);

        It should_delete_file_one = () => File.Exists(_testFileOne).ShouldBeFalse();

        It should_delete_file_two = () => File.Exists(_testFileTwo).ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        Cleanup stuff = () => {
            File.Delete(_testFileOne);
            File.Delete(_testFileTwo);
        };
    }
}