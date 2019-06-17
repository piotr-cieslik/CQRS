using System;
using System.Collections.Generic;
using System.Linq;

namespace Piotr.CQRS
{
    /// <summary>
    /// This class internally creates lookups of commands and queries definition.
    /// To ensure best possible performance try to create single instance of HandlerLookup at the application startup.
    /// However it's possible to have more than one instance of this class, I wouldn't recommend that.
    /// </summary>
    public abstract class HandlersLookup : IHandlersLookup
    {
        private ILookup<Type, HandlerDefinition> _commandsLookup;
        private ILookup<Type, HandlerDefinition> _queriesLookup;

        public HandlersLookup()
        {
            _commandsLookup = CommandHandlers().ToLookup(x => x.Type);
            _queriesLookup = QueryHandlers().ToLookup(x => x.Type);
        }

        public IEnumerable<Func<ICommand<TResult>, TResult>> Handler<TResult>(ICommand<TResult> command) =>
            _commandsLookup[command.GetType()]
                .Select(x => x.CommandHandler<TResult>());

        public IEnumerable<Func<IQuery<TResult>, TResult>> Handler<TResult>(IQuery<TResult> query) =>
            _queriesLookup[query.GetType()]
                .Select(x => x.QueryHandler<TResult>());

        protected abstract IEnumerable<HandlerDefinition> CommandHandlers();

        protected abstract IEnumerable<HandlerDefinition> QueryHandlers();

        protected HandlerDefinition Handler<TCommand, TResult>(Func<ICommandHandler<TCommand, TResult>> factory)
            where TCommand : ICommand<TResult>
        {
            return new HandlerDefinition(
                typeof(TCommand),
                command => factory().Handle((TCommand)command));
        }

        protected HandlerDefinition Handler<TQuery, TResult>(Func<IQueryHandler<TQuery, TResult>> factory)
            where TQuery : IQuery<TResult>
        {
            return new HandlerDefinition(
                typeof(TQuery),
                x => factory().Handle((TQuery)x));
        }

        protected sealed class HandlerDefinition
        {
            private readonly Func<object, object> _handler;

            public HandlerDefinition(Type type, Func<object, object> handler)
            {
                Type = type;
                _handler = handler;
            }

            public Type Type { get; }

            public Func<ICommand<TResult>, TResult> CommandHandler<TResult>() => x => (TResult)_handler(x);

            public Func<IQuery<TResult>, TResult> QueryHandler<TResult>() => x => (TResult)_handler(x);
        }
    }
}