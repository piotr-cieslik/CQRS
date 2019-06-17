using System.Collections.Generic;
using Piotr.CQRS.Results;
using Xunit;

namespace Piotr.CQRS.Tests
{
    public sealed class ResultsTests
    {
        [Fact]
        public void ReturnsSuccessResult()
        {
            var result =
                new Dispatcher(new HandlersLookup())
                    .Dispatch(new SuccessCommand());

            Assert.True(result.Success());
        }

        [Fact]
        public void ReturnsErrorResult()
        {
            var result =
                new Dispatcher(new HandlersLookup())
                    .Dispatch(new ErrorCommand());

            Assert.False(result.Success());
        }

        [Fact]
        public void ReturnsErrorsResult()
        {
            var result =
                new Dispatcher(new HandlersLookup())
                    .Dispatch(new ErrorsCommand());

            Assert.False(result.Success());
        }

        public class SuccessCommand : ICommand<CommandResult>
        {
        }

        public sealed class SuccessCommandHandler : ICommandHandler<SuccessCommand, CommandResult>
        {
            public CommandResult Handle(SuccessCommand operation)
            {
                return new Success();
            }
        }

        public class ErrorCommand : ICommand<CommandResult>
        {
        }

        public sealed class ErrorCommandHandler : ICommandHandler<ErrorCommand, CommandResult>
        {
            public CommandResult Handle(ErrorCommand operation)
            {
                return new Error("error");
            }
        }

        public class ErrorsCommand : ICommand<CommandResult>
        {
        }

        public sealed class ErrorsCommandHandler : ICommandHandler<ErrorsCommand, CommandResult>
        {
            public CommandResult Handle(ErrorsCommand operation)
            {
                return new Error(new[] { "error1", "error2" });
            }
        }

        public sealed class HandlersLookup : CQRS.HandlersLookup
        {
            protected override IEnumerable<HandlerDefinition> CommandHandlers()
            {
                yield return Handler(() => new SuccessCommandHandler());
                yield return Handler(() => new ErrorCommandHandler());
                yield return Handler(() => new ErrorsCommandHandler());
            }

            protected override IEnumerable<HandlerDefinition> QueryHandlers()
            {
                yield break;
            }
        }
    }
}
