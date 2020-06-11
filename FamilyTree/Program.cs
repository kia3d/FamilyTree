using Microsoft.VisualBasic.FileIO;
using System;
using System.Linq;

namespace FamilyTree
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("please specify full path to the input filename");
                Console.WriteLine("e.g. c:\\temp\\data.txt");

            }
            //in real setuations to be replaced replace with dependancy injection 
            var commandRunner = new CommandProcessor(new FamilyTree(), args[0]);
            try
            {
                commandRunner.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //in real scenarios log error detail 
                //and rethrow the exception;
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}

