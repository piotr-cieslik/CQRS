using CQRS.Internal;
using CQRS.Markers;

namespace CQRS
{
    public interface ICommand<TResult> : IOperation<TResult>, ICommand
    {
    }
}
