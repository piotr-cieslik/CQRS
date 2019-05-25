namespace CQRS
{
    public interface IOperationHandler<TOperation, TResult>
        where TOperation : IOperation<TResult>
    {
        TResult Handle(TOperation operation);
    }
}
