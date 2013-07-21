using System;
using System.IO;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Combinators))]
    internal class When_I_try_to_throw_an_exception_and_recover_by_creating_a_file {
        static ITry _result;
        static Action _throwException;
        static Action<Exception> _byCreatingFile;
        static string _recoverFile;

        Establish context = () => {
            _recoverFile = Path.GetTempFileName();

            _throwException = () => { throw new ArgumentException("Expected test exception."); };

            _byCreatingFile = error => File.Create(_recoverFile);
        };

        Because of = () => _result = Try.To(_throwException)
                                        .Recover(_byCreatingFile);

        It should_create_the_file = () => File.Exists(_recoverFile).ShouldBeTrue();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        Cleanup stuff = () => File.Delete(_recoverFile);
    }
}