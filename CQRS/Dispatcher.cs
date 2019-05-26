using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS
{
    public sealed class Dispatcher : IDispatcher
    {
        private readonly IHandlersLookup _lookup;

        public Dispatcher(IHandlersLookup lookup) => _lookup = lookup;

        public TResult Dispatch<TResult>(IOperation<TResult> operation)
        {
            var handlers = _lookup.Handler(operation);
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
