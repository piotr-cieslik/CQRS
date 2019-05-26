using CQRS;

namespace CQRS.Internal
{
    public interface IOperationHandler<TOperation, TResult>
        where TOperation : IOperation<TResult>
    {
        TResult Handle(TOperation operation);
    }
}
