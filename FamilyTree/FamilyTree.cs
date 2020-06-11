using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree
{
    public class FamilyTree : IFamilyTree
    {
        public static readonly string KingName = "Shan";
        public static readonly string QueenName = "Anga";

        private List<Node> Data { get; set; } = new List<Node>();

        //Just hardcoded root to add queen so we can start adding other from file
        private Node GenerateRoot()
        {
            if (Data.Any())
            {
                Data.Clear();
            }

            var queen = new Node()
            {
                Gender = Gender.Female,
                Mother = null,
                Name = QueenName,
                Partner = null,
            };
            return queen;
        }

        public FamilyTree()
        {
            var root = GenerateRoot();
            Data.Add(root);
            AddPartner(QueenName, KingName, Gender.Male);
        }

        private Node FindMother(string childName)
        {
            var node = FindByName(childName);
            if (node != null)
            {
                return node.Mother;
            }
            return null;
        }

        private Node FindFather(string childName)
        {
            var mother = FindMother(childName);
            if (mother != null)
            {
                return mother.Partner;
            }
            return null;
        }

        private List<Node> FindSiblings(string childName)
        {
            var childNode = FindByName(childName);
            var mother = childNode.Mother; ;
            if (mother == null)
            {
                return new List<Node>();
            }

            return Data.FindAll(x => x.Mother == mother && x != childNode);
        }

        private List<string> FindParentSiblings(string parentName, Func<Node, bool> predicate)
        {
            return FindSiblings(parentName).Where(predicate).Select(x => x.Name).ToList();
        }

        private List<string> FindInLaws(string name, Func<Node, bool> predicate)
        {
            var node = FindByName(name);
            if (node == null)
            {
                return new List<string>();
            }
            var siblingsOfPartner = FindSiblings(node.Partner.Name);
            var siblings = FindSiblings(name);

            //Sibling of partners are in laws
            var partnersSide = siblingsOfPartner.Where(x => predicate(x));

            //Partners of siblings of partners are in laws too
            var partnerSiblingsPartnersSide = siblingsOfPartner.Where(x => x.Partner != null && predicate(x.Partner)).Select(x => x.Partner);

            //My own siblings partners are in laws too
            var siblingsSide = siblings.Where(x => x.Partner != null && predicate(x.Partner));

            return siblingsSide.Union(partnersSide).Union(partnerSiblingsPartnersSide).Select(x => x.Name).ToList();
        }

        private List<string> FindKids(string name, Func<Node, bool> predicate)
        {
            var node = FindByName(name);

            if (node == null)
            {
                return new List<string>();
            }

            var mother = node.Gender == Gender.Male ? node.Partner : node;
            return Data.FindAll(x => x.Mother != null && x.Mother == mother).Where(predicate).Select(x => x.Name).ToList();
        }

        public List<string> GetRelationship(string name, Relationship relationship)
        {
            var empty = new List<string>();

            switch (relationship)
            {
                case Relationship.PaternalUncle:
                case Relationship.PaternalAunt:

                    var father = FindFather(name);
                    if (father == null)
                    {
                        return empty;
                    }

                    return FindParentSiblings(father.Name,
                        node => relationship == Relationship.PaternalUncle
                        ? node.Gender == Gender.Male
                        : node.Gender == Gender.Female);

                case Relationship.MaternalUncle:
                case Relationship.MaternalAunt:

                    var mother = FindMother(name);
                    if (mother == null)
                    {
                        return empty;
                    }

                    return FindParentSiblings(mother.Name,
                        x => relationship == Relationship.MaternalUncle
                        ? x.Gender == Gender.Male
                        : x.Gender == Gender.Female);

                case Relationship.SisterInLaw:
                    return FindInLaws(name, x => x.Gender == Gender.Female);

                case Relationship.BrotherInLaw:
                    return FindInLaws(name, x => x.Gender == Gender.Male);

                case Relationship.Son:
                    return FindKids(name, x => x.Gender == Gender.Male);

                case Relationship.Daughter:
                    return FindKids(name, x => x.Gender == Gender.Female);

                case Relationship.Siblings:
                    return FindSiblings(name).Select(x => x.Name).ToList();

                case Relationship.Partner:
                    var node = FindByName(name);
                    if (node == null)
                    {
                        throw new PersonNotFoundException();
                    }

                    return node.Partner != null ? new List<string> { node.Partner.Name } : empty;
                default:
                    throw new RelationshipNotFoundException();
            }
        }

        public Node FindByName(string Name)
        {
            if (Data != null && Data.Any())
            {
                return Data.FirstOrDefault(x => x.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase));
            }

            throw new Exception("Unexpected error: FamilyTree data is not set.");
        }

        public Node AddPartner(string targetName, string partnerName, Gender gender)
        {
            var target = FindByName(targetName);
            if (target.Gender == gender)
            {
                throw new Exception($"Please select a right Gender, {targetName} is also a {target.Gender}");
            }

            if (target == null)
            {
                throw new PersonNotFoundException();
            }

            var partner = new Node()
            {
                Gender = gender,
                Mother = null,
                Name = partnerName,
                Partner = target,
            };

            target.Partner = partner;
            Data.Add(partner);
            return partner;
        }

        public Node AddChild(string motherName, string childName, Gender gender)
        {

            var mother = FindByName(motherName);
            if (mother == null)
            {
                throw new PersonNotFoundException();
            }

            if (mother.Gender != Gender.Female)
            {
                throw new ChildAdditionFailedException();
            }
            var child = new Node()
            {
                Gender = gender,
                Mother = mother,
                Name = childName,
                Partner = null
            };
            Data.Add(child);
            return child;
        }
    }
}

