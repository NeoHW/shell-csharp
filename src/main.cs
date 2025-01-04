using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace CommandParser {
    public class CommandParser {
        private const string ExitCommand = "exit";
        private const string EchoCommand = "echo";
        private const int ExitCode = 0;
 
        public void Run() {
            while (true) {
                PrintUserInputLine();
                var userInput = Console.ReadLine();
                HandleUserInput(userInput); 
            }
        }

        private static void PrintUserInputLine() {
            Console.Write("$ ");
        }

        private void HandleUserInput(string? userInput) {
            if (string.IsNullOrWhiteSpace(userInput)) {
                Console.WriteLine("No command entered. Please try again.");
                return;
            }
            
            var (commandWord, args) = ExtractCommandAndArgs(userInput);
            ExecuteCommand(commandWord, args);
        }
        
        private (string commandWord, string args) ExtractCommandAndArgs(string command) {
            var parts = command.Split(" ", 2);
            var commandWord = parts.Length > 0 ? parts[0] : string.Empty;
            var args = parts.Length > 1 ? parts[1] : string.Empty;
            return (commandWord, args);
        }
        
        private void ExecuteCommand(string commandWord, string args) {
           switch (commandWord.ToLowerInvariant()) {
               case ExitCommand:
                   ExitApplication();
                   break;
               case EchoCommand:
                   ExecuteEchoCommand(args);
                   break;
               default:
                   Console.WriteLine($"{commandWord}: command not found");
                   break;
           }
        }

        private static void ExitApplication() {
            Environment.Exit(ExitCode);
        }

        private void ExecuteEchoCommand(string args) {
            Console.WriteLine(args);
        }
    }

    class Program {
        static void Main(string[] args) {
            var parser = new CommandParser();
            parser.Run();
        }
    }
}