using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piotr.CQRS.Results
{
    public sealed class Error : CommandResult
    {
        public Error(string error)
            : base(error)
        {
        }

        public Error(IEnumerable<string> errors)
            : base(errors)
        {
        }
    }
}