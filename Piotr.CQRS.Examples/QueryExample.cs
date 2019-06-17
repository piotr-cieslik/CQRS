using System;
using System.Collections.Generic;

namespace Piotr.CQRS.Examples
{
    public sealed class QueryExample
    {
        public void Run()
        {
            var handlersLookup = new HandlersLookup();
            var disaptcher = new Dispatcher(handlersLookup);
            var result = disaptcher.Dispatch(new GetRandomNumberQuery(1,100));
            Console.WriteLine(result);
        }

        // Definition of query
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

        // Definition of query handler
        public sealed class GetRandomNumberQueryHandler : IQueryHandler<GetRandomNumberQuery, int>
        {
            public int Handle(GetRandomNumberQuery query)
            {
                return new Random().Next(query.Min, query.Max);
            }
        }

        // Bind query and query handler
        public sealed class HandlersLookup : CQRS.HandlersLookup
        {
            protected override IEnumerable<HandlerDefinition> CommandHandlers()
            {
                yield break;
            }

            protected override IEnumerable<HandlerDefinition> QueryHandlers()
            {
                yield return Handler(() => new GetRandomNumberQueryHandler());
            }
        }
    }
}
