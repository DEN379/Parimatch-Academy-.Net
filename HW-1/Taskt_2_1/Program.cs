using System;

namespace Taskt_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Game rock-scissors-paper, made by Denys Sakadel");

            Console.WriteLine("Enter one of rock-scissors-paper: ");
            Console.WriteLine("Rules: Rock > Scissors > Paper > Rock");
            Console.WriteLine("You can leave the game by enter exit");
            String exit = "exit";
            int n_player_won = 0;
            int n_computer_won = 0;
            int n_none = 0;
            Random random = new Random();
            int r_number = 0;
            String player_rez = "";
            String computer_rez = "";


            while (player_rez != exit) { 
                r_number = random.Next(2);
                Console.WriteLine("~", 25);
                player_rez = Console.ReadLine();
                computer_rez = "";
                if(player_rez != "exit") switch (r_number)
                {
                    //default: Console.WriteLine("~",25);
                    case 0:
                        computer_rez = "Rock";
                        Console.WriteLine("Computer => " + computer_rez);
                        break;
                    case 1:
                        computer_rez = "Scissors";
                        Console.WriteLine("Computer => " + computer_rez);
                        break;
                    case 2:
                        computer_rez = "Paper";
                        Console.WriteLine("Computer => " + computer_rez);
                        break;
                }
                if (player_rez != "exit") switch (player_rez)
                {
                    case "Scissors":
                        switch (computer_rez)
                        {
                            case "Rock":
                                Console.WriteLine("You lost");
                                n_computer_won++;
                                break;
                            case "Scissors":
                                Console.WriteLine("None");
                                n_none++;
                                break;
                            case "Paper":
                                Console.WriteLine("You won");
                                n_player_won++;
                                break;
                            default: 
                                Console.WriteLine("You enter wrong word");
                                Console.WriteLine("Try to enter one of this words: Paper, Rock, Scissors");
                                break;
                        }
                        break;
                    case "Rock":
                        switch (computer_rez)
                        {
                            case "Rock":
                                Console.WriteLine("None");
                                n_none++;
                                break;
                            case "Scissors":
                                Console.WriteLine("You won");
                                n_player_won++;
                                break;
                            case "Paper":
                                Console.WriteLine("You lost");
                                n_computer_won++;
                                break;
                        }
                        break;
                    case "Paper":
                        switch (computer_rez)
                        {
                            case "Rock":
                                Console.WriteLine("You won");
                                n_player_won++;
                                break;
                            case "Scissors":
                                Console.WriteLine("You lost");
                                n_computer_won++;
                                break;
                            case "Paper":
                                Console.WriteLine("None");
                                n_none++;
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("You enter wrong word");
                        Console.WriteLine("Try to enter one of this words: Paper, Rock, Scissors");
                        break;
                }
            }
            Console.WriteLine($"You won {n_player_won} times");
            Console.WriteLine($"Computer won {n_computer_won} times");
            Console.WriteLine($"None {n_none} times");

            Console.WriteLine("Thank you for playing Game, see you soon, good bye!!!");
        }
    }
}
