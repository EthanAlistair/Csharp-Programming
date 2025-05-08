using System;
using static System.Console;
using System.Globalization;
using System.Linq.Expressions;

// more usings
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        // moved further down again to be after comparison 
        GetContestantData(contestants, thisyear);

        // chapter 14 addition
        // serialize contestants
        // creates new .ser file in current directory
        BinaryFormatter bFormatter = new BinaryFormatter();
        using (FileStream fs = new FileStream("Greenville.ser", FileMode.Create, FileAccess.Write))
        {
           bFormatter.Serialize(fs, contestants);
        }

        // commenting this out for chapter 14 addition       
        // display list of talents and contestant's name through input
        //GetLists(contestants);

        // chapter 14 addition - serialization
        // deserialize contestants
        Contestant[] serialcontestants;
        using (FileStream fs = new FileStream("Greenville.ser", FileMode.Open, FileAccess.Read))
        {
         serialcontestants = (Contestant[])bFormatter.Deserialize(fs);
        }

        GetLists(serialcontestants);
    }

    // method to get number of contestants with validation
    // chapter 11 addition add try-catch
    static int GetContestantNumber(string prompt)
    {
        int num;
        while (true)
        {
            Write(prompt);
            try
            {
                num = int.Parse(ReadLine());

                if (num >= 0 && num <= 30)
                    return num;

                throw new ArgumentOutOfRangeException();
            }

            catch (FormatException)
            {
                WriteLine("Number must be between 0 and 30");
            }

            catch (ArgumentOutOfRangeException)
            {
                WriteLine("Number must be between 0 and 30");
            }
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
            // chapter 11 addition try catch for talent code input

            char talentCode;
            while (true)
            {
                WriteLine("S = Singing | D = Dancing | M = Musical Instrument | O = Other");
                Write("Enter contestant #" + (i + 1) + " talent code: ");

                try
                {
                    string input = ReadLine();

                    // check if input is empty or more than 1 character
                    if (string.IsNullOrWhiteSpace(input) || input.Length != 1)
                        throw new FormatException();
                    
                    talentCode = char.ToUpper(input[0]);

                    if (Array.IndexOf(Contestant.talentCodes, talentCode) < 0)
                    {
                        WriteLine($"{input} is not a valid talent code. Assigned as Invalid");
                        talentCode = 'I';
                    }
                    break;
                }

                // catch exceptions
                catch (FormatException)
                {
                    WriteLine("Invalid input. Please try again.");
                }
            }


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
            // chapter 11 addition try-catch for list of contestants with respective talent
            try
            {
                // check if code is valid
                if (Array.IndexOf(Contestant.talentCodes, search) < 0)
                 throw new ArgumentException($"{input} is not a valid code");
                
                WriteLine($"Contestants with talent {Contestant.talentStrings[Array.IndexOf(Contestant.talentCodes, search)]} are: ");

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

            catch (ArgumentException e)
            {
                WriteLine(e.Message);
            }
        }
    }
}

// chapter 9 addition
// contestant class

// chapter 14 addition - serialization
[Serializable]
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
// chapter 14 addition - serialization
[Serializable]
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

// chapter 14 addition - serialization
[Serializable]
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

// chapter 14 addition - serialization
[Serializable]
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