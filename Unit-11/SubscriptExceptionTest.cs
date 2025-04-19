using System;
using static System.Console;
using System.Globalization;
class SubscriptExceptionTest
{
	static void Main()
	{
		// Write your code here
		// variables
		
		double [] array = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10,};
		int index;

		// loop

		while (true)
		{
			try
			{
				WriteLine("Enter the position of the array (0-9) Enter (99) to quit: ");
			string input = ReadLine();

			//ask user to input again if an integer is not entered
			if (!int.TryParse(input, out index))
			{
				WriteLine(" Invalid input, please try again.");
				continue;
			}

			if (index == 99)
			{
				break;
			}

			//output
			WriteLine($"Value of array at {index}: {array[index]}");
			}
			
			catch (IndexOutOfRangeException)
			{
				WriteLine("Index was outside the bounds of the array.");
			}
		}

	}
}