using CQRS.Internal;

namespace CQRS
{
    public interface ICommandHandler<TCommand, TResult> : IOperationHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
