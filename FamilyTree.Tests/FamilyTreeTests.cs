using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Tests
{
    [TestClass]
    public class FamilyTreeTests
    {
        private const string ChildName = "TestChild";
        private FamilyTree _familyTree = null;
        public FamilyTreeTests()
        {
            _familyTree = new FamilyTree();
        }

        [TestMethod]
        public void GivenRootNodesAreSetWhenAddingChildToMotherThenChildWillBeAdded()
        {
            var newNode = _familyTree.AddChild(FamilyTree.QueenName, ChildName, Gender.Female);
            var queenNode = _familyTree.FindByName(FamilyTree.QueenName);

            Assert.IsNotNull(newNode);
            Assert.AreEqual(newNode.Gender, Gender.Female);
            Assert.AreEqual(newNode.Mother, queenNode);
        }

        [TestMethod]
        public void GivenRootNodesAreSetWhenAddingChildToFatherThenExceptionThrown()
        {
            Assert.ThrowsException<ChildAdditionFailedException>(() => _familyTree.AddChild(FamilyTree.KingName, ChildName, Gender.Female));
        }

        [TestMethod]
        public void GivenSomeChildAddedWhenGettingSiblingRelationshipThenAllSiblingsShown()
        {
            var targetChild = ChildName + 0;
            var numberOfChildToAdd = 3;
            for (int i = 0; i < numberOfChildToAdd; i++)
            {
                _familyTree.AddChild(FamilyTree.QueenName, ChildName + i, Gender.Female);
            }

            var result = _familyTree.GetRelationship(targetChild, Relationship.Siblings);
            Assert.AreEqual(string.Join(" ", result), $"{ChildName + 1} {ChildName + 2}");
        }

        [TestMethod]
        public void GivenSomeChildAddedWhenGettingSonOrDautherRelationshipThenCorrectChildrenShown()
        {
            var son = _familyTree.AddChild(FamilyTree.QueenName, ChildName + 0, Gender.Male);
            var dauther = _familyTree.AddChild(FamilyTree.QueenName, ChildName + 1, Gender.Female);
            Assert.AreEqual(_familyTree.GetRelationship(FamilyTree.QueenName, Relationship.Son)[0], son.Name);
            Assert.AreEqual(_familyTree.GetRelationship(FamilyTree.QueenName, Relationship.Daughter)[0], dauther.Name);
        }

        private string NameGen
        {
            get { return Guid.NewGuid().ToString(); }
        }

        [TestMethod]
        public void GivenSomeChildAddedWhenGettingMaternalRelationshipThenCorrectPeopleShown()
        {
            var newWoman1 = _familyTree.AddChild(FamilyTree.QueenName, NameGen, Gender.Female);
            var maternalAunt = _familyTree.AddChild(FamilyTree.QueenName, NameGen, Gender.Female);
            var maternalUncle = _familyTree.AddChild(FamilyTree.QueenName, NameGen, Gender.Male);

            var targetKid = _familyTree.AddChild(newWoman1.Name, NameGen, Gender.Female);
            Assert.AreEqual(_familyTree.GetRelationship(targetKid.Name, Relationship.MaternalAunt)[0], maternalAunt.Name);
            Assert.AreEqual(_familyTree.GetRelationship(targetKid.Name, Relationship.MaternalUncle)[0], maternalUncle.Name);
        }

        [TestMethod]
        public void GivenSomeChildAddedWhenGettingPaternalRelationshipThenCorrectPeopleShown()
        {
            var newMan1 = _familyTree.AddChild(FamilyTree.QueenName, NameGen, Gender.Male);
            var newMan1sPartner = _familyTree.AddPartner(newMan1.Name, NameGen, Gender.Female);
            var maternalAunt = _familyTree.AddChild(FamilyTree.QueenName, NameGen, Gender.Female);
            var maternalUncle = _familyTree.AddChild(FamilyTree.QueenName, NameGen, Gender.Male);

            var targetKid = _familyTree.AddChild(newMan1sPartner.Name, NameGen, Gender.Female);
            Assert.AreEqual(_familyTree.GetRelationship(targetKid.Name, Relationship.PaternalAunt)[0], maternalAunt.Name);
            Assert.AreEqual(_familyTree.GetRelationship(targetKid.Name, Relationship.PaternalUncle)[0], maternalUncle.Name);
        }

        //we can unittest all other relationships sameway 
    }
}
