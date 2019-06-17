using System;
using System.Collections.Generic;

namespace Piotr.CQRS.Examples
{
    public sealed class MainExample
    {
        public void Run()
        {
            var handlersLookup = new HandlersLookup();
            var disaptcher = new Dispatcher(handlersLookup);
            var number = disaptcher.Dispatch(new GetRandomNumberQuery(1,100));
            var result = disaptcher.Dispatch(new WriteNumberToConsoleCommand(number));
        }

        public sealed class GetRandomNumberQuery : IQuery<int>
        {
            public GetRandomNumberQuery(int min, int max)
            {
                Min = min;
                Max = max;
            }

            public int Min { get; }

            public int Max { get; }
        }

        public sealed class GetRandomNumberQueryHandler : IQueryHandler<GetRandomNumberQuery, int>
        {
            public int Handle(GetRandomNumberQuery query)
            {
                return new Random().Next(query.Min, query.Max);
            }
        }

        public sealed class WriteNumberToConsoleCommand : ICommand<bool>
        {
            public WriteNumberToConsoleCommand(int number)
            {
                Number = number;
            }

            public int Number { get; }
        }

        public sealed class WriteNumberToConsoleCommandHandler : ICommandHandler<WriteNumberToConsoleCommand, bool>
        {
            public bool Handle(WriteNumberToConsoleCommand command)
            {
                Console.WriteLine(command.Number);
                return true;
            }
        }

        public sealed class HandlersLookup : CQRS.HandlersLookup
        {
            protected override IEnumerable<HandlerDefinition> CommandHandlers()
            {
                yield return Handler(() => new WriteNumberToConsoleCommandHandler());
            }

            protected override IEnumerable<HandlerDefinition> QueryHandlers()
            {
                yield return Handler(() => new GetRandomNumberQueryHandler());
            }
        }
    }
}
