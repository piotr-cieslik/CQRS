using System;
using System.Collections.Generic;
using Xunit;

namespace CQRS.Tests
{
    public sealed class DispatcherTests
    {
        [Fact]
        public void ThrowsExceptionWhenHandlerNotFound()
        {
            var handlerLookup = new HandlerLookup();
            Assert.Throws<InvalidOperationException>(
                () => new Dispatcher(handlerLookup).Dispatch(new Operation()));
        }

        [Fact]
        public void ThrowsExceptionWhenMoreThanOneHandlerFound()
        {
            var handlerLookup =
                new HandlerLookup(
                    new object(),
                    new object());
            Assert.Throws<InvalidOperationException>(
                () => new Dispatcher(handlerLookup).Dispatch(new Operation()));
        }

        [Fact]
        public void ReturnsResultWhenSingleHandlerFound()
        {
            var expected = new object();
            var handlerLookup = new HandlerLookup(expected);
            var result = new Dispatcher(handlerLookup).Dispatch(new Operation());
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

            IEnumerable<Func<TResult>> IHandlersLookup.Handler<TResult>(IOperation<TResult> operation)
            {
                foreach(var result in _results)
                {
                    yield return () => (TResult)result;
                }
            }
        }

        private sealed class Operation
            : IOperation<object>
        {
        }
    }
}
