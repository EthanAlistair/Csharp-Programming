using System;
using static System.Console;
using System.Globalization;
class PhotoDemo
{
    static void Main()
    {
		// Write your code here
        // standard photo sizes 8x10 / 10x12
        Photo basicPhoto = new Photo(8, 10);

        // custom photo size
        // (height, width)
        Photo customPhoto = new Photo(8, 9);

        // matted photo
        // (height, width, color)
        MattedPhoto matted = new MattedPhoto(10, 12, "white");

        // framed photo
        // (height, width, material, style)
        FramedPhoto framed = new FramedPhoto(8, 10, "silver", "modern");

        // output 
        WriteLine(basicPhoto);
        WriteLine(customPhoto);
        WriteLine(matted);
        WriteLine(framed);
    }
}

class Photo
{
    public int Width { get; set; }
    public int Height { get; set; }

    // protected price field
    protected double Price;

    public Photo(int width, int height)
    {
        Width = width;
        Height = height;

        // standard size prices
        if (width == 8 && height == 10)
            Price = 3.99;

        else if (width == 10 && height == 12)
            Price = 5.99;
        
        // custom size price
        else
            Price = 9.99;
    }

    public override string ToString()
    {
        return $"{GetType().Name} | {Width} X {Height} | Price: ${Price:F2}";
    }
}

class MattedPhoto : Photo
{
    public string Color { get; set; }

    public MattedPhoto(int width, int height, string color) : base(width, height)
    {
        Color = color;

        // increases price by 10
        Price += 10;
    }

    public override string ToString()
    {
        return $"{GetType().Name} - {Color} matting | {Width} X {Height} | Price: ${Price:F2}";
    }
}

class FramedPhoto : Photo
{
    public string Material { get; set; }
    public string Style { get; set; }

    public FramedPhoto(int width, int height, string material, string style) : base(width, height)
    {
        Material = material;
        Style = style;

        // increases price by 25
        Price += 25;
    }

    public override string ToString()
    {
        return $"{GetType().Name} - {Style}, {Material} frame. | {Width} X {Height} | Price: ${Price:F2}";
    }
}