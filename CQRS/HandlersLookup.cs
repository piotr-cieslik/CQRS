using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS
{
    public abstract class HandlersLookup : IHandlersLookup
    {
        public IEnumerable<Func<ICommand<TResult>, TResult>> Handler<TResult>(ICommand<TResult> command) =>
            CommandHandlers()
                .Where(x => x.Type.Equals(command.GetType()))
                .Select(x => x.Handler<TResult>());

        public IEnumerable<Func<IQuery<TResult>, TResult>> Handler<TResult>(IQuery<TResult> query) =>
            QueryHandlers()
                .Where(x => x.Type.Equals(query.GetType()))
                .Select(x => x.Handler<TResult>());

        protected abstract IEnumerable<CommandDefinition> CommandHandlers();

        protected abstract IEnumerable<QueryDefinition> QueryHandlers();

        protected CommandDefinition CommandHandler<TCommand, TResult>(Func<ICommandHandler<TCommand, TResult>> factory)
            where TCommand : ICommand<TResult>
        {
            return new CommandDefinition(
                typeof(TCommand),
                command => factory().Handle((TCommand)command));
        }

        protected QueryDefinition QueryHandler<TQuery, TResult>(Func<IQueryHandler<TQuery, TResult>> factory)
            where TQuery : IQuery<TResult>
        {
            return new QueryDefinition(
                typeof(TQuery),
                x => factory().Handle((TQuery)x));
        }

        protected sealed class CommandDefinition
        {
            private readonly Func<object, object> _handler;

            public CommandDefinition(Type type, Func<object, object> handler)
            {
                Type = type;
                _handler = handler;
            }

            public Type Type { get; }

            public Func<ICommand<TResult>, TResult> Handler<TResult>() => x => (TResult)_handler(x);
        }

        protected sealed class QueryDefinition
        {
            private readonly Func<object, object> _handler;

            public QueryDefinition(Type type, Func<object, object> handler)
            {
                Type = type;
                _handler = handler;
            }

            public Type Type { get; }

            public Func<IQuery<TResult>, TResult> Handler<TResult>() => x => (TResult)_handler(x);
        }
    }
}