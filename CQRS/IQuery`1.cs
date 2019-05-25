namespace CQRS
{
    public interface IQuery<TResult> : IOperation<TResult>
    {
    }
}
