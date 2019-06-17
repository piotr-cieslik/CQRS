using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Piotr.CQRS.Tests
{
    public sealed class HandlersLookupTests
    {
        [Fact]
        public void ReturnsZeroHandlers()
        {
            var command = new Command0();
            var handler = new TestHandlersLookup().Handler(command);
            Assert.Empty(handler);
        }

        [Fact]
        public void ReturnsOneHandlers()
        {
            var command = new Command1();
            var handler = new TestHandlersLookup().Handler(command);
            Assert.Single(handler);
        }

        [Fact]
        public void ReturnsTwoHandlers()
        {
            var command = new Command2();
            var handler = new TestHandlersLookup().Handler(command);
            Assert.Equal(2, handler.Count());
        }

        public sealed class TestHandlersLookup : HandlersLookup
        {
            protected override IEnumerable<HandlerDefinition> CommandHandlers()
            {
                yield return Handler(() => new CommandHandler1());
                yield return Handler(() => new CommandHandler2());
                yield return Handler(() => new CommandHandler2());
            }

            protected override IEnumerable<HandlerDefinition> QueryHandlers()
            {
                yield break;
            }
        }

        public sealed class Command0 : ICommand<object>
        {
        }

        public sealed class CommandHandler0 : ICommandHandler<Command0, object>
        {
            public object Handle(Command0 operation)
            {
                throw new NotImplementedException();
            }
        }

        public sealed class Command1 : ICommand<object>
        {
        }

        public sealed class CommandHandler1 : ICommandHandler<Command1, object>
        {
            public object Handle(Command1 operation)
            {
                throw new NotImplementedException();
            }
        }

        public sealed class Command2 : ICommand<object>
        {
        }

        public sealed class CommandHandler2 : ICommandHandler<Command2, object>
        {
            public object Handle(Command2 operation)
            {
                throw new NotImplementedException();
            }
        }
    }
}
