using System;
using System.IO;
using System.Linq;

namespace FamilyTree
{
    public class CommandProcessor
    {
        private string[] _commands = Array.Empty<string>();

        private IFamilyTree FamilyTree { get; }

        public CommandProcessor(IFamilyTree familyTree, string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"{filename} does not exist");
            }
            var fileData = File.ReadAllText(filename);
            _commands = fileData.Split(Environment.NewLine);
            FamilyTree = familyTree;
        }

        public void Start()
        {
            foreach (var command in _commands)
            {
                ExecuteCommand(command);
            }
        }
        private void ExecuteCommand(string command)
        {
            var commandArray = command.Split(" ");
            var parameters = commandArray.Skip(1).Take(commandArray.Length - 1).ToArray();
            var commandName = commandArray[0];

            switch (commandName)
            {
                case CommandConstants.ADD_CHILD:
                    AddChild(parameters);
                    break;
                case CommandConstants.GET_RELATIONSHIP:
                    GetRelationship(parameters);
                    break;
                case CommandConstants.ADD_PARTNER:
                    AddPartner(parameters);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Invalid Command {commandName}");
            }
        }

        private void AddChild(string[] parameters)
        {
            if (parameters.Count() != 3)
            {
                throw new ArgumentException("Add child needs 3 parameters");
            }
            if (!Enum.TryParse<Gender>(parameters[2], out var gender))
            {
                throw new ArgumentException("Gender parameter is invalid");
            }
            FamilyTree.AddChild(parameters[0], parameters[1], gender);
        }

        private void AddPartner(string[] parameters)
        {
            if (parameters.Count() != 3)
            {
                throw new ArgumentException("Add partner needs 3 parameters");
            }
            if (!Enum.TryParse<Gender>(parameters[2], out var gender))
            {
                throw new ArgumentException("Gender parameter is invalid");
            }

            FamilyTree.AddPartner(parameters[0], parameters[1], gender);
        }

        private void GetRelationship(string[] parameters)
        {
            if (parameters.Count() != 2)
            {
                throw new ArgumentException("Get relationship needs 2 parameters");
            }

            if (!Enum.TryParse<Relationship>(parameters[1].Replace("-", ""), out var relationship))
            {
                throw new ArgumentException("Requested relationsip parameter is invalid");
            }

            Console.WriteLine(string.Join(" ",FamilyTree.GetRelationship(parameters[0], relationship)));
        }
    }
}

