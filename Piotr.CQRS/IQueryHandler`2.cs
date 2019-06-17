using Piotr.CQRS.Internal;

namespace Piotr.CQRS
{
    public interface IQueryHandler<TQuery, TResult> : IOperationHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
