using System.Collections.Generic;

namespace FamilyTree
{
    public interface IFamilyTree
    {
        Node AddChild(string motherName, string childName, Gender gender);
        Node AddPartner(string targetName, string partnerName, Gender gender);
        Node FindByName(string Name);
        List<string> GetRelationship(string name, Relationship relationship);
    }
}