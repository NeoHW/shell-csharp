using System.Net;
using System.Net.Sockets;

Console.Write("$ ");

// Wait for user input
var userInputCommand = Console.ReadLine();
Console.WriteLine(ParseCommand(userInputCommand));

string ParseCommand(string? command) {
    return $"{command}: command not found";
}