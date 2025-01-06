using System.Text.RegularExpressions;

namespace CommandParserApp.Utilities;

public static class CommandParserUtils
{
   public static (string commandWord, List<string> args) ExtractCommandAndArgs(string userInput)
   {
      var args = new List<string>();
      var regex = new Regex(@"'(.*?)'|(\S+)", RegexOptions.Compiled);

      foreach (Match match in regex.Matches(userInput))
      {
         // Match either the quoted or the unquoted group
         if (match.Groups[1].Success)
         {
            args.Add(match.Groups[1].Value); // Quoted value
         }
         else if (match.Groups[2].Success)
         {
            args.Add(match.Groups[2].Value); // Unquoted value
         }
      }
      
      // First part is the command word, remaining are the arguments
      var commandWord = args.FirstOrDefault() ?? string.Empty;
      args.RemoveAt(0); // Remove the command word from args

      return (commandWord, args);
   } 
}