using ConsoleUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.WithoutGenerics
{
    public static class OriginalTextFileProcessor
    {
        internal static List<Person> Loadpeople(string filePath)
        {
            List<Person> output = new List<Person>();
            Person p;
            var lines = System.IO.File.ReadAllLines(filePath).ToList();

            // Remove the header rom
            lines.RemoveAt(0);

            foreach (var line in lines)
            {
                string[] vals = line.Split(',');
                p = new Person();

                p.FirstName = vals[0];
                p.IsAlive = bool.Parse(vals[1]);
                p.LastName = vals[2];

                output.Add(p);
            }

            return output;
        }

        internal static void SavePeople(List<Person> people, string filePath)
        {
            List<string> lines = new List<string>();

            // add a header row
            lines.Add("FirstName,IsAlive,LastName");

            foreach (var p in people)
            {
                lines.Add($"{ p.FirstName },{ p.IsAlive },{ p.LastName }");
            }

            System.IO.File.WriteAllLines(filePath, lines);
        }
    }
}
