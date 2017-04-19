using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFGame.Data;
using EFGame.Domain.Repositories;

namespace EFGame.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var executioner = new Executioner();
            executioner.Run();
        }

        public class Executioner
        {
            public Executioner()
            {
                _playerRepository = new PlayerRepository();
                _matchRepository = new MatchRepository();
                _missionRepository = new MissionRepository();
            }

            private readonly PlayerRepository _playerRepository;
            private readonly MatchRepository _matchRepository;
            private readonly MissionRepository _missionRepository;

            public void Run()
            {
                var choice = 0;
                PrintMenu();
                do
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            PrintMenu();
                            break;
                        case 2:
                            PrintAllPlayers();
                            break;
                        case 3:
                            PrintAllMatches();
                            break;
                        case 4:
                            CreatePlayer();
                            break;
                        case 5:
                            CreateMatch();
                            break;
                        case 6:
                            CreateMission();
                            break;
                        case 7:
                            DeletePlayer();
                            break;
                        case 8:
                            EditPlayer();
                            break;
                        case 9:
                            DeleteMatch();
                            break;
                        case 10:
                            DeleteMission();
                            break;
                    }
                } while (choice != 0);
            }

            private void PrintMenu()
            {
                Console.WriteLine("0 - Exit program");
                Console.WriteLine("1 - Print menu");
                Console.WriteLine("2 - Print all players");
                Console.WriteLine("3 - Print all matches");
                Console.WriteLine("4 - Add new player");
                Console.WriteLine("5 - Add new match");
                Console.WriteLine("6 - Add new mission");
                Console.WriteLine("7 - Delete a player");
                Console.WriteLine("8 - Edit player info");
                Console.WriteLine("9 - Delete a match");
                Console.WriteLine("10 - Delete a mission");

            }
            private void PrintAllPlayers()
            {
                _playerRepository.GetAllPlayers().ForEach(p => Console.WriteLine($"Id: {p.PlayerId} Username: {p.Username} Email: {p.Email} Password: {p.Password} MMR: {p.MMR}"));
            }

            private void PrintAllMatches()
            {
                _matchRepository.GetAllMatches().ForEach(m => Console.WriteLine($"Id: {m.MatchId} Name: {m.Name}"));
            }

            private int GenerateRandom()
            {
                Random rnd = new Random();
                return rnd.Next(1500, 2300);
            }
            private void CreatePlayer()
            {
                Console.WriteLine("Enter following information separated by a new line: username, email, password");
                var p = new Player()
                {
                    Username = Console.ReadLine(),
                    Email = Console.ReadLine(),
                    Password = Console.ReadLine(),
                    MMR = GenerateRandom()
                };
                _playerRepository.AddPlayer(p);
            }

            private void CreateMatch()
            {
                Console.WriteLine("Enter name for match:");
                var m = new Match()
                {
                    Name = Console.ReadLine()
                };
                Console.WriteLine("How many players are there in this match?");
                var counter = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the index of each player (separated by a new line):");
                List<int> indexes = new List<int>();
                for (var i = 0; i < counter; i++)
                {
                    indexes.Add(int.Parse(Console.ReadLine()));
                }
                _matchRepository.AddMatch(m, indexes);
            }

            private void CreateMission()
            {
                Console.WriteLine("Enter mission name and text separated by a new line:");
                var ms = new Mission()
                {
                    Name = Console.ReadLine(),
                    Text = Console.ReadLine()
                };
                Console.WriteLine("Enter name of match this mission belongs to:");
                var name = Console.ReadLine();
                _missionRepository.AddMission(ms, name);
            }

            private void DeletePlayer()
            {
                Console.WriteLine("Enter id of the player you want to delete:");
                var idPlayerToDelete = int.Parse(Console.ReadLine());
                _playerRepository.DeletePlayer(idPlayerToDelete);
            }

            private void EditPlayer()
            {
                Console.WriteLine("Enter the id of the player you want to edit:");
                var playerToEdit = new Player()
                {
                    PlayerId = int.Parse(Console.ReadLine())
                };
                Console.WriteLine("Enter new player name");
                playerToEdit.Username = Console.ReadLine();
                Console.WriteLine("Enter new player password:");
                playerToEdit.Password = Console.ReadLine();
                _playerRepository.UpdateExisting(playerToEdit);
            }

            private void DeleteMatch()
            {
                Console.WriteLine("Enter the id of the match you want to delete:");
                var idMatchToDelete = int.Parse(Console.ReadLine());
                _matchRepository.DeleteMatch(idMatchToDelete);
            }

            private void DeleteMission()
            {
                Console.WriteLine("Enter the id of the mission you want to delete:");
                var idMissionToDelete = int.Parse(Console.ReadLine());
                _missionRepository.DeleteMission(idMissionToDelete);
            }
        }

    }
}
