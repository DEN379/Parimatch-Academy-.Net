using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("My notes, made by Denys Sakadel");
            var list = JsonReader.ReadNotes();
            string command = "";
            while (!command.Trim().ToLower().Equals("exit"))
            {
                Console.WriteLine("1. Add a note");
                Console.WriteLine("2. Show note");
                Console.WriteLine("3. Find note");
                Console.WriteLine("4. Delete note");
                Console.WriteLine("5. Exit");
                command = Console.ReadLine();
                switch (command)
                {
                    case "1": AddNote(list);
                        break;
                    case "2":
                        Console.WriteLine("Enter id of note =>");
                        long id = 0;
                        try
                        {
                            id = int.Parse(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Wrong expression, enter pls right id number");
                            break;
                        }
                        Note.ShowNote(id, list, true);
                        break;
                    case "3": FindNote(list);
                        break;
                    case "4": DeleteNote(list);
                        break;
                    case "5":
                        command = "exit";
                        break;
                    default:
                        Console.WriteLine("You entered wrong expression, enter pls number 1-5");
                        break;
                }

            }
        }

        private static void AddNote(List<Note> list)
        {
            Console.WriteLine("Enter pls your note =>");
            string note = Console.ReadLine().Trim();
            if (String.IsNullOrEmpty(note))
            {
                Console.WriteLine("Note is empty, this note won't be create");
            }

            string title;
            if (note.Length < 32) title = note;
            else title = note.Substring(0, 32);
            long id = list.Select(n => n.Id).Max();
            Note note1 = new Note() { Body = note, Title = title, Date = DateTime.UtcNow, Id = id + 1 };
            list.Add(note1);
            Note.ToJson(list);
        }

        private static void DeleteNote(List<Note> list)
        {
            Console.WriteLine("Which note you want to delete? Enter id =>");
            long n = 0;
            try
            {
                n = long.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("You entered wrong expression, enter number");
                return;
            }
            Note note = Note.ShowNote(n, list, true);
            if (note == null)
            {
                Console.WriteLine("There is no note with this id");
                return;
            }
            Console.WriteLine("Do you realy want to delete this note? Enter - yes or no =>");
            string s = Console.ReadLine();
            if (s.Trim().ToLower().Equals("yes"))
            {
                list.Remove(note);
                Note.ToJson(list);
            }
        }

        private static void FindNote(List<Note> list)
        {
            Console.WriteLine("Enter find request => ");
            string s = Console.ReadLine();
            var findList = list.FindAll(n => n.Body.Contains(s));
            if (findList.Count == 0) Console.WriteLine("There is no notes with this request.");
            else findList.ForEach(n => Console.WriteLine($"id# {n.Id}. Title: {n.Title}, Create: {n.Date}"));
        }

        
        
    }
}
