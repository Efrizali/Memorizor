using System;
using System.Collections.Generic;

namespace Memorizor
{
    internal class Program
    {
        private static readonly List<List<String>> Studyables = new List<List<String>>();

        private static readonly Random rnd = new Random();

        static void Main(string[] args)
        {
            AddTerms();

            Study();
        }

        // Next Steps:
        // 
        // Create a system for reading a file to get the info
        //      Study groups (IEEE, Digital Signal, Ethernet Standards, etc)
        //      Header(Group, Question, Answer)
        //      Data
        // Create a system to write the file

        private static void Study()
        {
            Console.WriteLine("Begin!");
            while (true)
            {
                List<String> term = Studyables[rnd.Next(Studyables.Count)];
                Console.WriteLine($"\n{term[0]}\n");

                Console.Write("Press Enter to reveal");
                Console.ReadLine();

                Console.WriteLine($"\n{term[1]}");

                Console.Write("\n\nPress Enter For next Term");
                Console.ReadLine();
            }
        }

        private static void AddTerms()
        {
            Studyables.Add(new List<String> { "hi", "Meh" });
            Studyables.Add(new List<String> { "bye", "tired" });
        }
    }
}
