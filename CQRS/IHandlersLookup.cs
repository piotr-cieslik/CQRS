using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS
{
    public interface IHandlersLookup
    {
        IEnumerable<Func<TResult>> Handler<TResult>(ICommand<TResult> command);

        IEnumerable<Func<TResult>> Handler<TResult>(IQuery<TResult> query);
    }
}