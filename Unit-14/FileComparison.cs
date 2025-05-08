using System;
using static System.Console;
using System.IO;
class FileComparison
{
   static void Main()
   {
      // Write your code here
      
      // not entirely sure where the provided files are since they arent in the folders after unzipping
      // so i created my own quote.txt and quote.docx and filled them with random stuff to simulate this exercise
      // define the files
      string wordFile = "Quote.docx";
      string textFile = "Quote.txt";

      // create file info objects
      FileInfo word = new FileInfo(wordFile);
      FileInfo text = new FileInfo(textFile);

      // file size
      long wordSize = word.Length;
      long textSize = text.Length;

      // calculate percentage
      double ratio = ((double)textSize / wordSize) * 100;

      // output
      WriteLine($"The size of the Word file is {wordSize}");
      WriteLine($"and the size of the Notepad file is {textSize}");
      WriteLine($"The Notepad file is {ratio:F2}% of the size of the Word file");
   }
}