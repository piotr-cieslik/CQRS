using CQRS.Internal;

namespace CQRS
{
    public interface ICommand<TResult> : IOperation<TResult>, ICommand
    {
    }
}
