using System;
using System.Net;
using System.Threading;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "CatchWith")]
    internal class
        When_I_try_to_throw_an_argument_exception_catch_it_and_return_a_success_containing_zero_instead {
        static Func<int> _throw;
        static ITry<int> _result;
        static Func<ArgumentException, ITry<int>> _catch;

        Establish context = () => {
            _throw = () => { throw new ArgumentException("Expected test exception"); };
            _catch = error => new Success<int>(0);
        };

        Because of = () => _result = Try.To(_throw)
                                        .CatchWith(_catch);

        It should_contain_zero_in_the_success =
            () => _result.Value.ShouldEqual(0);

        It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }

    [Subject(typeof (NiceTry.Combinators), "Using")]
    internal class
        When_I_try_to_add_two_and_three_and_using_a_webclient_post_that_to_httpbin_and_return_the_response {
        static Func<string> _hello;
        static Func<WebClient> _withWebClient;
        static Func<string, WebClient, string> _sendToHttpBin;
        static ITry<string> _result;

        Establish context = () => {
            _hello = () => "hello";
            _withWebClient = () => new WebClient();
            _sendToHttpBin = (s, wc) => wc.UploadString("http://httpbin.org/post", s);
        };

        Because of = () => _result = Try.To(_hello)
                                        .Using(_withWebClient, _sendToHttpBin);

        It should_contain_the_response_in_the_success =
            () => _result.Value.ShouldContain("hello");

        It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }

    [Subject(typeof (NiceTry.Combinators), "Using")]
    internal class
        When_I_try_to_sleep_and_using_a_webclient_post_hello_to_httpbin_and_return_the_response {
        static Action _sleep;
        static Func<WebClient> _withWebClient;
        static Func<WebClient, string> _sendToHttpBin;
        static ITry<string> _result;

        Establish context = () => {
            _sleep = () => Thread.Sleep(50);
            _withWebClient = () => new WebClient();
            _sendToHttpBin = wc => wc.UploadString("http://httpbin.org/post", "hello");
        };

        Because of = () => _result = Try.To(_sleep)
                                        .Using(_withWebClient, _sendToHttpBin);

        It should_contain_the_response_in_the_success =
            () => _result.Value.ShouldContain("hello");

        It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}