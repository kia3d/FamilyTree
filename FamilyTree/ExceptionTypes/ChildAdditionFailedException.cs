using System;

namespace FamilyTree
{
    public class ChildAdditionFailedException : Exception
    {

        public ChildAdditionFailedException() : base("CHILD_ADDITION_FAILED")
        {
        }

        public ChildAdditionFailedException(string message) : base(message)
        {
        }

        public ChildAdditionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

