using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS
{
    public interface IDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);

        TResult Dispatch<TResult>(ICommand<TResult> command);
    }
}
