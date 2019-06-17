My implementation of CQRS (Command Query Responsibility Segregation) in C#. I successfully use this approach in single and medium size projects.

To successfully use this library, you have to follow these 4 steps:
1. Define command/query with handler.
2. Register handler into handlers lookup.
3. Create dispatcher based on handlers lookup.
4. Dispatch command/query.

Creation of command:
``` csharp
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
```

Creation of  query:
``` csharp
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
```

Creation of handlers lookup:
``` csharp
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
```

Creation of dispatcher:
``` csharp
var handlersLookup = new HandlersLookup();
var disaptcher = new Dispatcher(handlersLookup);
```

Usages:
``` csharp
var number = disaptcher.Dispatch(new GetRandomNumberQuery(1,100));
var result = disaptcher.Dispatch(new WriteNumberToConsoleCommand(number));
```

You can find more examples in the `Piotr.CQRS.Examples` project.

