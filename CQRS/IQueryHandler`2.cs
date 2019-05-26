using CQRS.Internal;

namespace CQRS
{
    public interface IQueryHandler<TQuery, TResult> : IOperationHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
