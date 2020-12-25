using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Task_1_2
{
    public enum PlayerRank
    {
        Private = 2, 
        Lieutenant = 21, 
        Captain = 25, 
        Major = 29, 
        Colonel = 33, 
        General = 39, 
    }
    public interface IPlayer 
    { 
        public int Age { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public PlayerRank Rank { get; } 
    }
    class Player : IPlayer
    {
        public int Age { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public PlayerRank Rank { get; }

        public Player(int age, string firstName, string lastName, PlayerRank rank)
        {
            Age = age;
            FirstName = firstName;
            LastName = lastName;
            Rank = rank;
        }
    }


    public class AgeComparer : IComparer<Player>
    {
        int IComparer<Player>.Compare(Player x, Player y)
        {
            if (ReferenceEquals(x, y)) return 0;
            else if (ReferenceEquals(null, y)) return 1;
            else if (ReferenceEquals(null, x)) return -1;
            else return (x.Age).CompareTo(y.Age);
        }
    }
    public class EqualityComparer : IEqualityComparer<Player>
    {
        bool IEqualityComparer<Player>.Equals(Player x, Player y)
        {
            if (ReferenceEquals(x, y)) return true;
            else return (x.Age == y.Age) && (x.FirstName == y.FirstName) && (x.LastName == y.LastName) && (x.Rank == y.Rank);
        }

        int IEqualityComparer<Player>.GetHashCode(Player obj)
        {
            return HashCode.Combine(obj.Age, obj.FirstName, obj.LastName, (int)obj.Rank);
        }
    }
    public class NameComparer : IComparer<Player>
    {
        int IComparer<Player>.Compare(Player x, Player y)
        {
            if (ReferenceEquals(x, y)) return 0;
            else if (ReferenceEquals(null, y)) return 1;
            else if (ReferenceEquals(null, x)) return -1;

            int rez_first_name = string.Compare(x.FirstName, y.FirstName, StringComparison.Ordinal);
            if (rez_first_name != 0) return rez_first_name;
            return string.Compare(x.LastName, y.LastName, StringComparison.Ordinal);
        }
    }
    public class RankComparer : IComparer<Player>
    {
        int IComparer<Player>.Compare(Player x, Player y)
        {
            if (ReferenceEquals(x, y)) return 0;
            else if (ReferenceEquals(null, y)) return 1;
            else if (ReferenceEquals(null, x)) return -1;
            else return (x.Rank).CompareTo(y.Rank);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("3 sorting and 1 comparator, made by Denys Sakadel");

            List<Player> players = new List<Player>();
            players.Add(new Player(29, "Ivan", "Ivanenko", PlayerRank.Captain));
            players.Add(new Player(19, "Peter", "Petrenko", PlayerRank.Private));
            players.Add(new Player(59, "Ivan", "Ivanov", PlayerRank.General));
            players.Add(new Player(52, "Ivan", "Snezko", PlayerRank.Lieutenant));
            players.Add(new Player(34, "Alex", "Zeshko", PlayerRank.Colonel));
            players.Add(new Player(29, "Ivan", "Ivanenko", PlayerRank.Captain));
            players.Add(new Player(19, "Peter", "Petrenko", PlayerRank.Private));
            players.Add(new Player(34, "Vasiliy", "Sokol", PlayerRank.Major));
            players.Add(new Player(31, "Alex", "Alexeenko", PlayerRank.Major));

            var list_whithout_dubles = players.Distinct(new EqualityComparer());
            players = list_whithout_dubles.ToList();

            Console.WriteLine("Sorted by age =>");
            players.Sort(new AgeComparer());
            foreach(Player p in players)
            {
                Console.WriteLine(p.Age + " years, " + p.FirstName + " " + p.LastName + ": " + p.Rank);
            }

            Console.WriteLine("Sorted by name =>");
            players.Sort(new NameComparer());
            foreach (Player p in players)
            {
                Console.WriteLine(p.FirstName + " " + p.LastName + ": " + p.Age + " years, "+ p.Rank);
            }

            Console.WriteLine("Sorted by rank =>");
            players.Sort(new RankComparer());
            foreach (Player p in players)
            {
                Console.WriteLine(p.Rank + ": " + p.FirstName + " " + p.LastName + " " + p.Age + " years " );
            }
        }
    }
}
