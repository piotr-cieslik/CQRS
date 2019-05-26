using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS
{
    public interface IHandlersLookup
    {
        IEnumerable<Func<TResult>> Handler<TResult>(IOperation<TResult> operation);
    }
}