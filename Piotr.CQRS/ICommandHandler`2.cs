using Piotr.CQRS.Internal;

namespace Piotr.CQRS
{
    public interface ICommandHandler<TCommand, TResult> : IOperationHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
