using System;
using static System.Console;
using System.Globalization;

class GreenvilleRevenue
{
    static void Main()
    {
        // Get number of contestants for last year and this year
        int lastyear = GetContestantNumber("Enter number of contestants last year (between 0-30) >> ");
        int thisyear = GetContestantNumber("Enter number of contestants this year (between 0-30) >> ");

        // Updated for Chapter 9 - Use Contestant class instead of separate arrays
        Contestant[] contestants = new Contestant[thisyear];

        // Calculate revenue
        int entranceFee = 25;
        int revenue = thisyear * entranceFee;

        // output contestant information
        WriteLine("\nLast year's competition had " + lastyear + " contestants, and this year's has " + thisyear + " contestants.");
        WriteLine("Revenue expected this year is {0}", revenue.ToString("C", CultureInfo.GetCultureInfo("en-US")));

        // show comparison of last year and this year
        DisplayRelationship(lastyear, thisyear);

        // get contestant details
        //moved further down again to be after comparison 
        GetContestantData(contestants, thisyear);

        // display list of talents and contestant's name through input
        GetLists(contestants);
    }

    // method to get number of contestants with validation
    static int GetContestantNumber(string prompt)
    {
        int num;
        while (true)
        {
            Write(prompt);
            if (int.TryParse(ReadLine(), out num) && num >= 0 && num <= 30)
                return num;

            WriteLine("Invalid number of contestants. Try again.");
        }
    }

    // method to compare this year's and last year's number of contestants
    static void DisplayRelationship(int lastyear, int thisyear)
    {
        if (thisyear > lastyear)
        {
            if (thisyear > lastyear * 2)
                WriteLine("\nThe competition is more than twice as big as last year!");
            else
                WriteLine("\nThe competition is bigger than ever!");
        }
        else
        {
            WriteLine("\nA tighter race this year! Come out and cast your vote!");
        }
    }

    // prompt user for contestant details
    static void GetContestantData(Contestant[] contestants, int thisyear)
    {
        // talent type counters
        int 
        sing = 0, 
        dance = 0, 
        music = 0, 
        other = 0;

        for (int i = 0; i < thisyear; i++)
        {
            // get contestant name
            Write("Enter contestant #" + (i + 1) + " name: ");
            string name = ReadLine();

            // get and validate talent code
            WriteLine("S = Singing | D = Dancing | M = Musical Instrument | O = Other");
            Write("Enter contestant #" + (i + 1) + " talent code: ");

            string input = ReadLine();
            while (string.IsNullOrWhiteSpace(input) || input.Length != 1)
            {
                Write("Invalid input. Try again: ");
                input = ReadLine();
            }
            char talentCode = input[0];

            // chapter 10 addition - ask user for contestant age
            Write("Enter contestant age: ");
            int age = Convert.ToInt32(ReadLine());

            Contestant c;
            if (age <= 12)
                c = new ChildContestant(name, talentCode);
            else if (age <= 17)
                c = new TeenContestant(name, talentCode);
            else
                c = new AdultContestant(name, talentCode);

        
            contestants[i] = c;

            // new talent count using TalentCode and switch/case for ease of use
            switch (c.TalentCode)
            {
                case 'S': sing++; break;
                case 'D': dance++; break;
                case 'M': music++; break;
                case 'O': other++; break;
            }
        }

        // output talent counts
        WriteLine("\nThe types of talent are:");
        WriteLine("Singing:              " + sing);
        WriteLine("Dancing:              " + dance);
        WriteLine("Musical Instrument:   " + music);
        WriteLine("Other:                " + other);

    }

    // method to display contestants based on selected talent code
    static void GetLists(Contestant[] contestants)
    {
        while (true)
        {
            WriteLine("\nEnter a talent code to see a list of contestants with that talent.");
            WriteLine("S/s = Singing | D/d = Dancing | M/m = Musical Instrument | O/o = Other | Z = Quit Application");
            Write("\nEnter talent code: ");

            string input = ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                WriteLine("\nInvalid talent code. Try again.");
                continue;
            }

            // convert input to uppercase for consistency
            char search = char.ToUpper(input[0]);

            if (search == 'Z')
                break; // Quit the application

            // display header for selected talent type
            if (search == 'S') WriteLine("\nContestants with Singing talent are:");
            else if (search == 'D') WriteLine("\nContestants with Dancing talent are:");
            else if (search == 'M') WriteLine("\nContestants with Musical Instrument talent are:");
            else if (search == 'O') WriteLine("\nContestants with Other talent are:");
            else
            {
                WriteLine("\nInvalid talent code. Try again.");
                continue;
            }

            // loop through the contestants array and print matching names
            bool found = false;

            foreach (Contestant c in contestants)
            {
                if (c.TalentCode == search)
                {
                    // chapter 10 addition - changed to output tostring data instead of just name to include their age group and fee
                    WriteLine(c.ToString());
                    found = true;
                }
            }

            if (!found)
            {
                WriteLine("No contestants with that talent.");
            }
        }
    }
}

// chapter 9 addition
// contestant class
class Contestant
{
    // static arrays to hold talent codes and descriptions
    public static char[] talentCodes = { 'S', 'D', 'M', 'O' };
    public static string[] talentStrings = { "Singing", "Dancing", "Musical Instrument", "Other" };
   
    // property to hold contestant name
    public string Name { get; set; }

    // private field for talent code
    // underscore before the name denotes it is private
    private char _talentCode;

    // get and set talent code with validation
    public char TalentCode
    {
        get { return _talentCode; }
        set 
        {
            // Convert to uppercase for consistency
            char upperValue = char.ToUpper(value);

            // Check if the value is a valid talent code
            if (Array.IndexOf(talentCodes, upperValue) >= 0)
                _talentCode = upperValue;
            else
                _talentCode = 'I'; // 'I' for Invalid
        }
    }

    // talent description - Read-Only property so no set
    public string Talent
    {
        get
        {
            int index = Array.IndexOf(talentCodes, _talentCode);
            if (index >= 0)
                return talentStrings[index];
            else
                return "Invalid";
        }
    }

    // initialize name and talent code
    public Contestant(string name, char talentCode)
    {
        Name = name;
        TalentCode = talentCode; // uses the setter with validation
    }

    // chapter 10 addition to contestant class - Added Fee
    public double Fee { get; set; }
}

// Chapter 10 addition - subclasses
class ChildContestant : Contestant
{
    public ChildContestant(string name, char talentCode) : base(name, talentCode)
    {
        Fee = 15;
    }

    // override containing age category and fee price
    public override string ToString()
    {
        return $"Child Contestant {Name} {TalentCode} | Fee {Fee.ToString("C")}";
    }
}

class TeenContestant : Contestant
{
    public TeenContestant(string name, char talentCode) : base(name, talentCode)
    {
        Fee = 20;
    }

    // override containing age category and fee price
    public override string ToString()
    {
        return $"Teen Contestant {Name} {TalentCode} | Fee {Fee.ToString("C")}";
    }
}

class AdultContestant : Contestant
{
    public AdultContestant(string name, char talentCode) : base(name, talentCode)
    {
        Fee = 30;
    }

    // override containing age category and fee price
    public override string ToString()
    {
        return $"Adult Contestant {Name} {TalentCode} | Fee {Fee.ToString("C")}";
    }
}