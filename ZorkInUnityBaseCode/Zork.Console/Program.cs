using System.IO;
using Newtonsoft.Json;
using System.Speech.Synthesis;

namespace Zork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string defaultGameFilename = "Zork.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultGameFilename);

            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameFilename));

            SpeechSynthesizer Text = new SpeechSynthesizer();
            ConsoleInputService input = new ConsoleInputService();
            ConsoleOutputService output = new ConsoleOutputService();

            output.WriteLine(string.IsNullOrWhiteSpace(game.WelcomeMessage) ? "Welcome to Zork!" : game.WelcomeMessage);

            game.Start(input, output);

            output.WriteLine(game.Player.Location);
            Game.Look(game);

            Room previousRoom = game.Player.Location;

            while (game.IsRunning)
            {
                //Refactor this into Games.cs

                if (previousRoom != game.Player.Location)
                {
                    output.WriteLine(game.Player.Location);
                    //Game.Look(game);
                    previousRoom = game.Player.Location;
                }

                output.Write("\n> ");
                input.ProcessInput();
            }

            output.WriteLine(string.IsNullOrWhiteSpace(game.ExitMessage) ? "Thank you for playing!" : game.ExitMessage);
        }

        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}