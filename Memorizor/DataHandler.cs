using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorizor
{
    internal class DataHandler
    {
        public static void WriteNewStudyables(string path, List<string> groups, List<Studyable> newStudyables)
        {
            string groupOutput = "";

            groups.ForEach(group =>
            {
                groupOutput += "|" + group;
            });

            groupOutput = groupOutput.Remove(0, 1);

            string headings = "Group|Question|Answer";


            string tempPath = "temp.csv";

            using (StreamReader reader = File.OpenText(path))
            {
                using (StreamWriter writer = File.CreateText(tempPath))
                {
                    writer.WriteLine(groupOutput);
                    writer.WriteLine(headings);

                    reader.ReadLine();
                    reader.ReadLine();

                    string? s = "";

                    while ((s = reader.ReadLine()) != null)
                    {
                        writer.WriteLine(s);
                    }

                    foreach (Studyable studyable in newStudyables)
                    {
                        string studyableString = "";

                        studyableString += studyable.Group;
                        studyableString += "|" + studyable.Question;
                        studyableString += "|" + studyable.Answer;

                        writer.WriteLine(studyableString);
                    }
                }
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.Move(tempPath, path);
        }

        public static List<Studyable> GetStudyables(string path)
        {
            List<Studyable> studyables = new List<Studyable>();

            using (StreamReader reader = File.OpenText(path))
            {
                reader.ReadLine();
                reader.ReadLine();

                string? s = "";

                while ((s = reader.ReadLine()) != null)
                {
                    List<string> items = new List<string>();

                    items.AddRange(s.Split('|'));

                    studyables.Add(new Studyable(items[0], items[1], items[2]));
                }
            }


            return studyables;
        }

        public static List<Studyable> GetStudyablesFromGroups(string path, List<string> groups)
        {
            List<Studyable> studyables = new List<Studyable>();

            using (StreamReader reader = File.OpenText(path))
            {
                reader.ReadLine();
                reader.ReadLine();

                string? s = "";

                while ((s = reader.ReadLine()) != null)
                {
                    List<string> items = new List<string>();

                    items.AddRange(s.Split('|'));

                    if (!groups.Contains(items[0])) continue;

                    studyables.Add(new Studyable(items[0], items[1], items[2]));
                }
            }


            return studyables;
        }

        public static List<string> GetGroups(string path)
        {
            List<string> output = new List<string>();

            using (StreamReader reader = File.OpenText(path))
            {
                string groups = reader.ReadLine();

                output.AddRange(groups.Split("|"));
            }

            return output;
        }
    }
}
