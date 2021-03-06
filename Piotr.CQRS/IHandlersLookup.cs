﻿using System;
using System.Collections.Generic;

namespace Piotr.CQRS
{
    public interface IHandlersLookup
    {
        IEnumerable<Func<ICommand<TResult>, TResult>> Handler<TResult>(ICommand<TResult> command);

        IEnumerable<Func<IQuery<TResult>, TResult>> Handler<TResult>(IQuery<TResult> query);
    }
}