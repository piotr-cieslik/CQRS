using CQRS.Internal;

namespace CQRS
{
    public interface IQuery<TResult> : IOperation<TResult>, IQuery
    {
    }
}
