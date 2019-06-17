namespace Piotr.CQRS
{
    public interface IDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);

        TResult Dispatch<TResult>(ICommand<TResult> command);
    }
}
