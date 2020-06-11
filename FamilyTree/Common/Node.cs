using System;

namespace FamilyTree
{
    public class Node
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public Node Partner { get; set; }
        public Node Mother { get; set; }
    }
}

