using System.Net;
using System.Net.Sockets;

namespace CommandParser {
    public class CommandParser {
        public void run() {
            while (true)
            {
               PrintUserInputLine();
               HandleUserInput(); 
            } 
        }

        private void PrintUserInputLine() {
            Console.Write("$ ");
        }

        private void HandleUserInput() {
            // Wait for user input
            var userInputCommand = Console.ReadLine();
            Console.WriteLine(ParseCommand(userInputCommand));
        }
        
        private string ParseCommand(string? command) {
            return $"{command}: command not found";
        }
    }

    class Program {
        static void Main(string[] args) {
           var parser = new CommandParser();
           parser.run();
        }
    }
}