using Piotr.CQRS.Internal;
using Piotr.CQRS.Markers;

namespace Piotr.CQRS
{
    public interface ICommand<TResult> : IOperation<TResult>, ICommand
    {
    }
}
