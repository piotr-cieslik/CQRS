using System;
using System.Collections.Generic;
using Xunit;

namespace CQRS.Tests
{
    public sealed class DispatcherTests
    {
        [Fact]
        public void ThrowsExceptionWhenCommandHandlerNotFound()
        {
            Assert.Throws<InvalidOperationException>(
                () =>
                    new Dispatcher(
                        new HandlerLookup())
                    .Dispatch(new Command()));
        }

        [Fact]
        public void ThrowsExceptionWhenQueryHandlerNotFound()
        {
            Assert.Throws<InvalidOperationException>(
                () =>
                    new Dispatcher(
                        new HandlerLookup())
                    .Dispatch(new Query()));
        }

        [Fact]
        public void ThrowsExceptionWhenMoreThanOneCommandHandlerFound()
        {
            Assert.Throws<InvalidOperationException>(
                () => 
                    new Dispatcher(
                        new HandlerLookup(
                            new object(),
                            new object()))
                    .Dispatch(new Command()));
        }

        [Fact]
        public void ThrowsExceptionWhenMoreThanOneQueryHandlerFound()
        {
            Assert.Throws<InvalidOperationException>(
                () =>
                    new Dispatcher(
                        new HandlerLookup(
                            new object(),
                            new object()))
                    .Dispatch(new Query()));
        }

        [Fact]
        public void ReturnsResultWhenSingleCommandHandlerFound()
        {
            var expected = new object();
            var result =
                new Dispatcher(
                    new HandlerLookup(expected))
                .Dispatch(new Command());
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReturnsResultWhenSingleQueryHandlerFound()
        {
            var expected = new object();
            var result =
                new Dispatcher(
                    new HandlerLookup(expected))
                .Dispatch(new Query());
            Assert.Equal(expected, result);
        }

        private sealed class HandlerLookup
            : IHandlersLookup
        {
            private readonly object[] _results;

            public HandlerLookup(params object[] results)
            {
                _results = results;
            }

            IEnumerable<Func<TResult>> IHandlersLookup.Handler<TResult>(ICommand<TResult> _)
            {
                foreach(var result in _results)
                {
                    yield return () => (TResult)result;
                }
            }

            IEnumerable<Func<TResult>> IHandlersLookup.Handler<TResult>(IQuery<TResult> _)
            {
                foreach (var result in _results)
                {
                    yield return () => (TResult)result;
                }
            }
        }

        private sealed class Command : ICommand<object>
        {
        }

        private sealed class Query : IQuery<object>
        {
        }
    }
}
