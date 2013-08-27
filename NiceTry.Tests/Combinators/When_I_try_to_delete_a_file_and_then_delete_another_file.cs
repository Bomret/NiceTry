using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "AndThen")]
    internal class When_I_try_to_delete_a_file_and_then_delete_another_file
    {
        private static Action _deleteFileOne;
        private static Func<ITry, ITry> _deleteFileTwo;
        private static ITry _result;
        private static string _testFileOne;
        private static string _testFileTwo;

        private Establish context = () =>
        {
            _testFileOne = Path.GetTempFileName();
            _testFileTwo = Path.GetTempFileName();

            _deleteFileOne = () => File.Delete(_testFileOne);
            _deleteFileTwo = t => Try.To(() => File.Delete(_testFileTwo));
        };

        private Because of = () => _result = Try.To(_deleteFileOne)
                                                .AndThen(_deleteFileTwo);

        private It should_delete_file_one = () => File.Exists(_testFileOne).ShouldBeFalse();

        private It should_delete_file_two = () => File.Exists(_testFileTwo).ShouldBeFalse();

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        private Cleanup stuff = () =>
        {
            File.Delete(_testFileOne);
            File.Delete(_testFileTwo);
        };
    }
}