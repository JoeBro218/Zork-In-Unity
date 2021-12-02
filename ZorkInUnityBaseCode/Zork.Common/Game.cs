using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Zork.Common;

namespace Zork
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler GameStop;

        public World World { get; private set; }

        public string StartingLocation { get; set; }
        
        public string WelcomeMessage { get; set; }
        
        public string ExitMessage { get; set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        [JsonIgnore]
        public bool IsRunning { get; set; }

        public IInputService Input { get; set; }

        public IOutputService Output { get; set; }

        [JsonIgnore]
        public Dictionary<string, Command> Commands { get; private set; }

        public Game(World world, Player player)
        {
            World = world;
            Player = player;

            Commands = new Dictionary<string, Command>()
            {
                { "QUIT", new Command("QUIT", new string[] { "QUIT", "Q", "BYE" }, Quit) },
                { "LOOK", new Command("LOOK", new string[] { "LOOK", "L" }, Look) },
                { "SCORE", new Command("SCORE", new string[] {"SCORE" }, Score) },
                { "REWARD", new Command("REWARD", new string[] {"REWARD", "R"}, Reward) },
                { "HELP", new Command("HELP", new string[] {"HELP", "H", "INFO", "INFORMATION", "I"}, Help) },
                { "NORTH", new Command("NORTH", new string[] { "NORTH", "N" }, game => Move(game, Directions.North)) },
                { "SOUTH", new Command("SOUTH", new string[] { "SOUTH", "S" }, game => Move(game, Directions.South)) },
                { "EAST", new Command("EAST", new string[] { "EAST", "E"}, game => Move(game, Directions.East)) },
                { "WEST", new Command("WEST", new string[] { "WEST", "W" }, game => Move(game, Directions.West)) },
            };
        }

        public void Start(IInputService input, IOutputService output)
        {
            Assert.IsNotNull(input);
            Input = input;
            Input.InputReceived += InputReceivedHandler;

            Assert.IsNotNull(output);
            Output = output;

            Player.Moves += 1;

            IsRunning = true;
        }

        private void InputReceivedHandler(object sender, string commandString)
        {

            Command foundCommand = null;
            foreach (Command command in Commands.Values)
            {
                if (command.Verbs.Contains(commandString))
                {
                    foundCommand = command;
                    break;
                }
            }

            if (foundCommand != null)
            {
                Player.Moves += 1;
                foundCommand.Action(this);
                Output.WriteLine(" ");
            }
            else
            {
                Output.WriteLine("Unknown command.");
            }
        }

        private static void Move(Game game, Directions direction)
        {
            if (game.Player.Move(direction) == false)
            {
                game.Output.WriteLine("The way is shut!");
            }

            if (game.LastRoom != game.Player.Location)
            {
                Game.Look(game);
                game.LastRoom = game.Player.Location;
            }
        }

        private static void Score(Game game)
        {
            game.Output.WriteLine($"Your Score is: {game.Player.Score}, and you have made {game.Player.Moves} moves.");
        }

        private static void Reward(Game game)
        {
            Random RanNum = new Random();
            int Num = RanNum.Next(1, 11);
            if (Num == 1)
            {
                game.Output.WriteLine("A rock. It's worthless.");
            }
            else if (Num > 1 && Num < 10)
            {
                game.Output.WriteLine("Woah! Free Coin!");
                game.Player.Score += 1;
            }
            else if (Num >= 10)
            {
                game.Output.WriteLine("Shinny Gold Egg!");
                game.Player.Score += 10;
            }
        }

        private static void Help(Game game)
        {
            game.Output.WriteLine("  Type North, South, East, or West to move in a given direction. \n  Type Look to observe the current room you are in. \n  Type Score to check the number of points you have the amount of moves made. \n  Type Reward to give yourself a random amount of points. \n  Type Help to bring up the list of commands. \n  Type Quit to exit the game.");
        }

        public static void Look(Game game) => game.Output.WriteLine(game.Player.Location.Description);

        private static void Quit(Game game)
        {
            game.IsRunning = false;
            game.GameStop?.Invoke(game, EventArgs.Empty);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) => Player = new Player(World, StartingLocation);

        private Room LastRoom;
    }
}