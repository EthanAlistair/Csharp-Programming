using System;
using static System.Console;
using System.Globalization;
class TestSoccerPlayer
{
	static void Main()
	{
		// Write your code here
		SoccerPlayer NewPlayer = new SoccerPlayer("Christer Ronaldo", 7, 929, 381); //(name, jerseynum, goals, assists)
		NewPlayer.showStats();
	}
}

class SoccerPlayer
{
	//variables
	public string Name;
	public int JerseyNum;
	public int Goals;
	public int Assists;

	//Object
	public SoccerPlayer(string name, int jerseyNum, int goals, int assists)
	{
		Name = name;
		JerseyNum = jerseyNum;
		Goals = goals;
		Assists = assists;
	}

	public void showStats()
	{
		WriteLine("Name: " + Name);
		WriteLine("Jersey Number: " + JerseyNum);
		WriteLine("# of Goals: " + Goals);
		WriteLine("# of Assists: " + Assists);

	}
}