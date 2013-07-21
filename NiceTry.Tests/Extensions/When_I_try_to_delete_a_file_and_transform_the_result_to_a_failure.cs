using System;
using System.IO;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Combinators))]
    internal class When_I_try_to_delete_a_file_and_transform_the_result_to_a_failure {
        static Action _deleteFile;
        static string _testFile;
        static Func<ITry> _returnFailure;
        static Func<Exception, ITry> _fromErrorToSuccess;
        static ITry _result;

        Establish context = () => {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);

            _returnFailure = () => new Failure(new ArgumentException("Expected test exception."));
            _fromErrorToSuccess = error => new Success();
        };

        Because of = () => _result = Try.To(_deleteFile)
                                        .Transform(_returnFailure, _fromErrorToSuccess);

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();

        Cleanup stuff = () => File.Delete(_testFile);
    }
}