using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Retry))]
    internal class When_I_retry_to_delete_a_file_with_an_invalid_path_up_to_two_times {
        static Action _deleteFile;
        static ITry _result;

        Establish context = () => { _deleteFile = () => File.Delete(string.Empty); };

        Because of = () => _result = Retry.To(_deleteFile);

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}