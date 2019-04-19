﻿using ConsoleUI.Models;
using ConsoleUI.WithGenerics;
using ConsoleUI.WithoutGenerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            DemonstrateTextFileStorage();

            Console.ReadKey();
        }

        private static void DemonstrateTextFileStorage()
        {
            List<Person> people = new List<Person>();
            List<LogEntry> logs = new List<LogEntry>();

            string peopleFile = @"C:\Temp\people.csv";
            string logFile = @"C:\Temp\logs.csv";

            PopulateLists(people, logs);

            /* New way - generics */
            GenericTextFileProcessor.SaveToTextFile<Person>(data: people, filePath: peopleFile);
            GenericTextFileProcessor.SaveToTextFile<LogEntry>(data: logs, filePath: logFile);

            var newPeople = GenericTextFileProcessor.LoadFromTextFile<Person>(peopleFile);

            foreach (var p in newPeople)
            {
                Console.WriteLine($"{ p.FirstName } { p.LastName } (IsAlive = { p.IsAlive })");
            }

            var newLogs = GenericTextFileProcessor.LoadFromTextFile<LogEntry>(logFile);

            foreach (var log in newLogs)
            {
                Console.WriteLine($"{ log.ErrorCode }: { log.Message } at { log.TimeOfEvent.ToShortTimeString() }");
            }

            /* Old way - non-generics */

            //OriginalTextFileProcessor.SaveLogs(logs, logFile);

            //var newLogs = OriginalTextFileProcessor.LoadLogs(logFile);

            //foreach (var log in newLogs)
            //{
            //    Console.WriteLine($"{ log.ErrorCode }: { log.Message } at { log.TimeOfEvent.ToShortTimeString() }");
            //}

            //OriginalTextFileProcessor.SavePeople(people, peopleFile);

            //var newPeople = OriginalTextFileProcessor.Loadpeople(peopleFile);

            //foreach (var p in newPeople)
            //{
            //    Console.WriteLine($"{ p.FirstName } { p.LastName } (IsAlive = { p.IsAlive })");
            //}
        }

        private static void PopulateLists(List<Person> people, List<LogEntry> logs)
        {
            people.Add(new Person { FirstName = "Gas", LastName = "Bawn" });
            people.Add(new Person { FirstName = "Ben", LastName = "Soyan", IsAlive = false });
            people.Add(new Person { FirstName = "Carl", LastName = "Olfan" });

            logs.Add(new LogEntry { Message = "I blew up", ErrorCode = 9976 });
            logs.Add(new LogEntry { Message = "I'm too awesome", ErrorCode = 1337 });
            logs.Add(new LogEntry { Message = "I was tired", ErrorCode = 2222 });
        }
    }
}
