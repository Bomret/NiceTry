using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Retry))]
    internal class When_I_retry_to_delete_a_file_up_to_two_times_that_succeeds_the_second_time {
        static Action _deleteFile;
        static int _try;
        static string _testFile;
        static ITry _result;

        Establish context = () => {
            _testFile = Path.GetTempFileName();

            _deleteFile = () => {
                _try += 1;

                if (_try < 2)
                    throw new ArgumentException("Expected test exception.");

                File.Delete(_testFile);
            };
        };

        Because of = () => _result = Retry.To(_deleteFile);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}