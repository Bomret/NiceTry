using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Retry), "To")]
    internal class When_I_retry_to_delete_a_file_up_to_two_times_which_succeeds_the_first_time {
        static Action _deleteFile;
        static string _testFile;
        static ITry _result;

        Establish context = () => {
            _testFile = Path.GetTempFileName();

            _deleteFile = () => File.Delete(_testFile);
        };

        Because of = () => _result = Retry.To(_deleteFile);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}