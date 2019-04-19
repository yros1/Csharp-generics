using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.WithGenerics
{
    public static class GenericTextFileProcessor
    {
        public static List<T> LoadFromTextFile<T>(string filePath) where T : class, new()
        {
            List<string> lines = System.IO.File.ReadAllLines(filePath).ToList();
            List<T> output = new List<T>();
            T entry = new T();                          // create ne instance of type T, where T type must have ab empty constractor, therefore 'new()' limiter when definig a class.
            var cols = entry.GetType().GetProperties(); // useage of reflection

            // Check to be sure we have at least one header row and one data row
            if (lines.Count < 2)
            {
                throw new IndexOutOfRangeException("The file was either empty or missing.");
            }

            // Splits the header into one colun header per row
            var headers = lines[0].Split(',');

            // Removes the header row from the lines so we don't
            // have to worry about skipping over that first row.
            lines.RemoveAt(0);

            foreach (var row in lines)
            {
                entry = new T();

                // Splits the row into individual columns. Now the index of this row matches the index of the header so
                // the FirstName column header lines up with the FirstName value in this row.
                var vals = row.Split(',');

                // Lops through each header entry so we can compare that against the list of columns from reflection.
                // Once we get the matching column, we can do the "SetValue" method to see the column value
                // for our entry variable to the vals item at the same index as this particular header.

                for (var i = 0; i < headers.Length; i++)
                {
                    foreach (var col in cols)
                    {
                        if (col.Name == headers[i])
                        {
                            col.SetValue(entry, Convert.ChangeType(vals[i], col.PropertyType));
                        }
                    }
                }

                output.Add(entry);
            }

            return output;
        }

        public static void SaveToTextFile<T>(List<T> data, string filePath) where T : class
        {
            List<string> lines = new List<string>();
            StringBuilder line = new StringBuilder();

            if (data == null || data.Count == 0)
            {
                throw new ArgumentNullException("data", "You must populate the data parameter with at least o");
            }

            var cols = data[0].GetType().GetProperties();

            // Loop through each column and gets the name so it can comma
            // seperate it into the header row.
            foreach (var col in cols)
            {
                line.Append(col.Name);
                line.Append(",");
            }

            // Adds the colukn header entries to the first line (removing
            // the last comma from the end first).
            lines.Add(line.ToString().Substring(0, line.Length - 1));

            foreach (var row in data)
            {
                line = new StringBuilder();

                foreach (var col in cols)
                {
                    line.Append(col.GetValue(row));
                    line.Append(",");
                }

                // adds the row tothe set lines (removing
                // the last comma from the end first)
                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }

            System.IO.File.WriteAllLines(filePath, lines);
        }
    }
}
