using System;

namespace FamilyTree
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException() : base("PERSON_NOT_FOUND")
        {
        }

        public PersonNotFoundException(string message) : base(message)
        {
        }

        public PersonNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

