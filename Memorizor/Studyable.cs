using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorizor
{
    internal class Studyable : IComparable<Studyable>
    {
        public string Group {  get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public Studyable(string group, string question, string answer)
        {
            Group = group;
            Question = question;
            Answer = answer;
        }

        public int CompareTo(Studyable? other)
        {
            if (other == null) return 1;

            if (other == this) return 0;

            return Group.CompareTo(other.Group);
        }

        public static List<String> GetGroups(List<Studyable> studyables)
        {
            var groups = new List<String>();

            foreach (Studyable studyable in studyables)
            {
                if (!groups.Contains(studyable.Group))
                    groups.Add(studyable.Group);
            }

            return groups;
        }
    }
}
