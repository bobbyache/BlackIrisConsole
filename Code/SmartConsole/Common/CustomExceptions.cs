using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackIris.Common.Exceptions
{
    /// <summary>
    /// Multiple verbs were found in the command. A command should only have one verb.
    /// </summary>
    public class MultipleVerbsFoundException : ApplicationException
    {
        public MultipleVerbsFoundException() { }

        public MultipleVerbsFoundException(string message)
            : base(message) { }

        public MultipleVerbsFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Multiple verbs were found in the command. A command should only have one verb.
    /// </summary>
    public class InvalidVerbPositionException : ApplicationException
    {
        public InvalidVerbPositionException() { }

        public InvalidVerbPositionException(string message)
            : base(message) { }

        public InvalidVerbPositionException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Multiple verbs were found in the command. A command should only have one verb.
    /// </summary>
    public class DuplicateSwitchException : ApplicationException
    {
        public DuplicateSwitchException() { }

        public DuplicateSwitchException(string message)
            : base(message) { }

        public DuplicateSwitchException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Multiple verbs were found in the command. A command should only have one verb.
    /// </summary>
    public class VerbNotFoundException : ApplicationException
    {
        public VerbNotFoundException() { }

        public VerbNotFoundException(string message)
            : base(message) { }

        public VerbNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
