using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS
{
    public sealed class Dispatcher : IDispatcher
    {
        private readonly IHandlersLookup _lookup;

        public Dispatcher(IHandlersLookup lookup) => _lookup = lookup;

        public TResult Dispatch<TResult>(IQuery<TResult> query) => DispatchOperation(query, _lookup.Handler(query));

        public TResult Dispatch<TResult>(ICommand<TResult> command) => DispatchOperation(command, _lookup.Handler(command));

        private TResult DispatchOperation<TResult>(
            IOperation<TResult> operation,
            IEnumerable<Func<TResult>> handlers)
        {
            var enumerator = handlers.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException($"Handler not found for {operation.GetType()}");
            }

            var handler = enumerator.Current;
            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException($"More than one handler found for {operation.GetType()}");
            }

            return handler();
        }
    }
}