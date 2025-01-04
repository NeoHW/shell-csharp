using System.Net;
using System.Net.Sockets;

namespace CommandParser {
    public class CommandParser {
        private const string ExitCommand = "exit 0";
 
        public void Run() {
            while (true) {
                PrintUserInputLine();
                HandleUserInput(); 
            } 
        }

        private static void PrintUserInputLine() {
            Console.Write("$ ");
        }

        private void HandleUserInput() {
            var userInputCommand = Console.ReadLine();
            Console.WriteLine(ParseCommand(userInputCommand));
        }
        
        private string ParseCommand(string? command) {
            if (command == ExitCommand) {
                Environment.Exit(0);
            }
            return $"{command}: command not found";
        }
    }

    class Program {
        static void Main(string[] args) {
            var parser = new CommandParser();
            parser.Run();
        }
    }
}