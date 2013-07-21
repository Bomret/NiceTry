﻿using System;
using System.IO;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_delete_a_file_and_register_for_completion {
        static Action _deleteFile;
        static ITry _result;
        static string _testFile;

        Establish context = () => {
            _testFile = Path.GetTempFileName();
            _deleteFile = () => File.Delete(_testFile);
        };

        Because of = () => Try.To(_deleteFile)
                              .WhenComplete(result => _result = result);

        It should_not_contain_an_exception = () => _result.Error.ShouldBeNull();

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        Cleanup stuff = () => File.Delete(_testFile);
    }
}