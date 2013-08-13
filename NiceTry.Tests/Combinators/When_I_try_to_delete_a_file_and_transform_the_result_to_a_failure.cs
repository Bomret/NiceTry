using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Transform")]
    internal class When_I_try_to_delete_a_file_and_transform_the_result_to_a_failure {
        private static Action _deleteFile;
        private static string _testFile;
        private static Func<ITry> _returnFailure;
        private static Func<Exception, ITry> _fromErrorToSuccess;
        private static ITry _result;

        private Establish context = () => {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);

            _returnFailure = () => new Failure(new ArgumentException("Expected test exception."));
            _fromErrorToSuccess = error => new Success();
        };

        private Because of = () => _result = Try.To(_deleteFile)
                                                .Transform(_returnFailure, _fromErrorToSuccess);

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();

        private Cleanup stuff = () => File.Delete(_testFile);
    }
}