using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS
{
    public interface IDispatcher
    {
        TResult Dispatch<TResult>(IOperation<TResult> command);
    }
}
