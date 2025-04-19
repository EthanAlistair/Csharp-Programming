using System;
using static System.Console;
using System.Globalization;
class SwimmingWaterTemperature
{
	static void Main()
	{
		// Write your code here
		// loop to continously prompt user for data
		while (true)
		{
			// prompt for water temperature
			WriteLine("Please enter the water temperature. (Enter '999' to quit)");
			string input = ReadLine();

			// asks user to enter again if a number is not entered
			if(!double.TryParse(input, out double temperature))
			{
				WriteLine("Invalid input, please try again.");
				continue;
			}

			// quit program
			if (temperature == 999)
			{
				break;
			}

			try
			{
				bool isComf = isComfortable(temperature);
				if (isComf)
				{
					WriteLine($"{temperature} degrees is comfortable for swimming.");
				}
				else
				{
					WriteLine($"{temperature} degrees is not comfortable.");
				}
			}
			catch (ArgumentException)
			{
				WriteLine("value does not fall within the expected range.");
			}

		}
	}

	static bool isComfortable(double temperature)
	{
		if (temperature < 32 || temperature > 212)
		{
			throw new ArgumentException();
		}

		return temperature >= 70 && temperature <= 85;
	}
}
