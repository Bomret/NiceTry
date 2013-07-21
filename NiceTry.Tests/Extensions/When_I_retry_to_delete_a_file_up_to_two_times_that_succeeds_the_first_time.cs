using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Retry))]
    internal class When_I_retry_to_delete_a_file_up_to_two_times_that_succeeds_the_first_time {
        static Action _delete;
        static string _testFile;
        static ITry _result;

        Establish context = () => {
            _testFile = Path.GetTempFileName();

            _delete = () => File.Delete(_testFile);
        };

        Because of = () => _result = Retry.To(_delete);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}