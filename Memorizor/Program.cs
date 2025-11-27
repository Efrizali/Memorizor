using System;
using System.Collections.Generic;

namespace Memorizor
{
    internal class Program
    {
        private static readonly List<Studyable> Studyables = new List<Studyable>();
        private static readonly List<string> Groups = new List<string>();
        private static readonly Random rnd = new Random();

        private static readonly string StudyablesPath = "C:\\My Files\\Code\\Memorizor\\Memorizor\\Memorizor\\Studyables.csv";

        static void Main(string[] args)
        {
            // Groups are useful, so they're being loaded first
            LoadGroups(new List<string> ());

            while (true)
            {
                switch (MenuOptions())
                {
                    case 0:
                        Environment.Exit(67);
                        break;
                    case 1:
                        Study();
                        break;
                    case 2:
                        AddNewTerms();
                        break;
                        
                }
            }
        }

        private static int MenuOptions()
        {
            Console.WriteLine("\n0. Exit");
            Console.WriteLine("1. Study");
            Console.WriteLine("2. Add New Terms");

            return CMDInput.GetSpecificIntFromUser(0, 2);
        }

        private static void Study()
        {
            Console.WriteLine("Select Groups to Study");

            List<string> groupsToStudy = new List<string>();

            List<string> availableGroups = new List<string>();
            availableGroups.AddRange(Groups);
            while (true)
            {
                int groupOption = SelectGroupFromList(availableGroups, false, true);

                if (groupOption == -3) return;
                if (groupOption == -2) break;

                if (groupOption == -1)
                {
                    groupsToStudy.AddRange(availableGroups);
                    break;
                }

                groupsToStudy.Add(availableGroups[groupOption]);
                availableGroups.RemoveAt(groupOption);
            }

            LoadGroups(groupsToStudy);

            Console.WriteLine("\nBegin!");
            while (true)
            {
                Studyable term = Studyables[rnd.Next(Studyables.Count)];
                Console.WriteLine($"\n{term.Question}\n");

                Console.Write("Press Enter to reveal");
                Console.ReadLine();

                Console.WriteLine($"\n{term.Answer}");

                Console.WriteLine("\n\nPress Enter For next Term or Type 'exit' to exit");
                string? exit = Console.ReadLine();

                if (exit == null) continue;
                
                if (exit.ToLower() == "exit") break;
            }
        }

        private static int SelectGroupFromList(List<string> groupList, bool addNew, bool allGroups)
        {
            Console.WriteLine("\nSelect Group");
            Console.WriteLine("0. Exit");

            int i = 1;

            if (allGroups)
            {
                Console.WriteLine($"{i}. Done Choosing Terms");
                i++;
                Console.WriteLine($"{i}. Select All Groups");
                i++;
            }

            foreach (string group in groupList)
            {
                Console.WriteLine($"{i}. {group}");

                i++;
            }

            if (addNew)
            {
                Console.WriteLine($"{i}. Add New Group");

                return CMDInput.GetSpecificIntFromUser(0, Groups.Count() + 1) - 1;
            }

            if (allGroups)
            {
                return CMDInput.GetSpecificIntFromUser(0, Groups.Count() + 2) - 3;
            }

            return CMDInput.GetSpecificIntFromUser(0, Groups.Count()) - 1; 
        }

        private static void AddNewTerms()
        { 
            int groupOption = SelectGroupFromList(Groups, true, false);

            if (groupOption == -1) return;

            string? group = null;
            string? question = null;
            string? answer = null;
            List<Studyable> newTerms = new List<Studyable>();

            if (groupOption < Groups.Count())
            {
                group = Groups[groupOption];
            }
            else
            {
                while (group == null)
                {
                    Console.Write("New Group Name: ");
                    group = CMDInput.GetStringFromUser();
                }
                
                if (!Groups.Contains(group)) Groups.Add(group);
            }

            while (true)
            {
                while (question == null)
                {
                    Console.WriteLine("\nEnter done when done or cancel to cancel ALL input terms (only on question field)");
                    Console.Write("Question: ");
                    question = CMDInput.GetStringFromUser();
                }

                if (question.ToLower() == "done") break;
                if (question.ToLower() == "cancel") return;

                while (answer == null)
                {
                    Console.Write("Answer: ");
                    answer = CMDInput.GetStringFromUser();
                }

                newTerms.Add(new Studyable(group, question, answer));

                question = null;
                answer = null;
            }

            DataHandler.WriteNewStudyables(StudyablesPath, Groups, newTerms);
        }


        // Methods from here down should work even in a non console app
        private static void LoadGroups(List<string> groups)
        {
            Studyables.Clear();
            Groups.Clear();

            Studyables.AddRange(DataHandler.GetStudyablesFromGroups(StudyablesPath, groups));
            Groups.AddRange(DataHandler.GetGroups(StudyablesPath));

        }
    }
}
