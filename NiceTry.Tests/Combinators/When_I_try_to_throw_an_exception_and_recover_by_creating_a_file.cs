using System;
using System.IO;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Recover")]
    internal class When_I_try_to_throw_an_exception_and_recover_by_creating_a_file {
        private static ITry _result;
        private static Action _throwException;
        private static Action<Exception> _byCreatingFile;
        private static string _recoverFile;

        private Establish context = () => {
            _recoverFile = Path.GetTempFileName();

            _throwException = () => { throw new ArgumentException("Expected test exception."); };

            _byCreatingFile = error => File.Create(_recoverFile);
        };

        private Because of = () => _result = Try.To(_throwException)
                                                .Recover(_byCreatingFile);

        private It should_create_the_file = () => File.Exists(_recoverFile).ShouldBeTrue();

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        private Cleanup stuff = () => File.Delete(_recoverFile);
    }
}