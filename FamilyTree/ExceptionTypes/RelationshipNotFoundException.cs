using System;

namespace FamilyTree
{
    public class RelationshipNotFoundException : Exception
    {
        public RelationshipNotFoundException() : base("NONE")
        {

        }

        public RelationshipNotFoundException(string message) : base(message)
        {
        }

        public RelationshipNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

