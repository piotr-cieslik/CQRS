namespace CQRS
{
    public interface ICommand<TResult> : IOperation<TResult>
    {
    }
}
