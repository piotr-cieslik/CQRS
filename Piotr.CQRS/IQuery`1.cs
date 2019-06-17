using Piotr.CQRS.Internal;
using Piotr.CQRS.Markers;

namespace Piotr.CQRS
{
    public interface IQuery<TResult> : IOperation<TResult>, IQuery
    {
    }
}
