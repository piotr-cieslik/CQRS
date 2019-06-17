using System;

namespace Piotr.CQRS
{
    public sealed class Dispatcher : IDispatcher
    {
        private readonly IHandlersLookup _lookup;

        public Dispatcher(IHandlersLookup lookup) => _lookup = lookup;

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            var enumerator = _lookup.Handler(query).GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException($"Handler not found for {query.GetType()}");
            }

            var handler = enumerator.Current;
            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException($"More than one handler found for {query.GetType()}");
            }

            return handler(query);
        }

        public TResult Dispatch<TResult>(ICommand<TResult> command)
        {
            var enumerator = _lookup.Handler(command).GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException($"Handler not found for {command.GetType()}");
            }

            var handler = enumerator.Current;
            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException($"More than one handler found for {command.GetType()}");
            }

            return handler(command);
        }
    }
}