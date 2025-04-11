using System;
using static System.Console;
using System.Globalization;

class LetterDemo
{
    static void Main()
    {
        // Write your code here
        // create and edit details of a regular letter
        Letter letter = new Letter("Ethan Alistair", "05/18/2003");
        WriteLine("Letter:");
        WriteLine(letter.ToString());

        // create and edit details ofa certified letter 
        CertifiedLetter certifiedLetter = new CertifiedLetter("Ethan Pangga", "05/18/2003", "123456789");
        WriteLine("\nCertified Letter:");
        WriteLine(certifiedLetter.ToString());
    }
}

class Letter
{
    public string Name { get; set; }
    public string Date { get; set; }

    public Letter(string name, string date)
    {
        Name = name;
        Date = date;
    }

    // overrides tostring method and returns/displys the letter
    public override string ToString()
    {
        return $"{GetType()} - Name: {Name}, Date Mailed: {Date}";
    }
}

class CertifiedLetter : Letter
{
    public string TrackingNumber { get; set; }

    public CertifiedLetter(string name, string date, string trackingNumber)
        : base(name, date)
    {
        TrackingNumber = trackingNumber;
    }

    // overrides tostring method and returns/displys the letter with tracking number
    public override string ToString()
    {
        return $"{GetType()} - Name: {Name}, Date Mailed: {Date}, Tracking Number: {TrackingNumber}";
    }
}
