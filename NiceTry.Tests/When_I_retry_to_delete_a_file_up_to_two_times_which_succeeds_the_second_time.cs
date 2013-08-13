using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Retry), "To")]
    internal class When_I_retry_to_delete_a_file_up_to_two_times_which_succeeds_the_second_time {
        private static Action _deleteFileButFailTheFirstTime;
        private static int _try;
        private static string _testFile;
        private static ITry _result;

        private Establish context = () => {
            _testFile = Path.GetTempFileName();

            _deleteFileButFailTheFirstTime = () => {
                _try += 1;

                if (_try < 2) {
                    throw new ArgumentException("Expected test exception.");
                }

                File.Delete(_testFile);
            };
        };

        private Because of = () => _result = Retry.To(_deleteFileButFailTheFirstTime);

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}