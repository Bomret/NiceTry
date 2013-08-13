using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions),"WhenComplete")]
    internal class When_I_try_to_delete_a_file_and_register_for_completion {
        private static Action _deleteFile;
        private static ITry _result;
        private static string _testFile;

        private Establish context = () => {
            _testFile = Path.GetTempFileName();

            _deleteFile = () => File.Delete(_testFile);
        };

        private Because of = () => Try.To(_deleteFile)
                                      .WhenComplete(result => _result = result);

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        private Cleanup stuff = () => File.Delete(_testFile);
    }
}