using CQRS.Internal;
using CQRS.Markers;

namespace CQRS
{
    public interface IQuery<TResult> : IOperation<TResult>, IQuery
    {
    }
}
