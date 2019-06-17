using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piotr.CQRS.Results
{
    public abstract class CommandResult
    {
        private readonly IEnumerable<string> _errors;

        public CommandResult()
            : this(Enumerable.Empty<string>())
        {
        }

        public CommandResult(string error)
            : this(new[] { error })
        {
        }

        public CommandResult(IEnumerable<string> errors)
        {
            _errors = errors;
        }

        public bool Success() => !_errors.Any();

        public string Error() => _errors.First();

        public IEnumerable<string> Errors() => _errors;
    }
}