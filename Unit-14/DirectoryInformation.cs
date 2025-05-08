using System;
using static System.Console;
using System.IO;
class DirectoryInformation
{
   static void Main()
   {
      // Write your code here

      // Tested by creating test folders and files in the .../bin/... folder where the program is located

      // store initial prompt
      string prompt = "Enter a directory >> ";

      // loop to continously prompt user for directories
      while (true)
      {
         Write(prompt);
         string input = ReadLine();

         // end loop if user types "end"
         if (input == "end")
         {
            break;
         }

         // check if directory exists
         if (Directory.Exists(input))
         {
            string[] files = Directory.GetFiles(input);
            WriteLine($"{input} contains the following files");
            
            if (files.Length == 0)
            {
               WriteLine("No files found in this directory");
            }

            else
            {
               foreach (string file in files)
               {
                  WriteLine($" {file}");
               }
            }
         }

         // if doesnt exist
         else
         {
            WriteLine($"Directory {input} does not exist");
         }

         //change prompt
         prompt = "Enter another directory or type 'end' to quit >> ";
         WriteLine();
      }
   }
}