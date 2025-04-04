using System;
using static System.Console;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
class TestClassifiedAd
{
	static void Main()
	{
		// Write your code here
		
		ClassifiedAd ad1 = new ClassifiedAd("Used Cars", 100);
		ad1.showAd();

		ClassifiedAd ad2 = new ClassifiedAd("Help Wanted", 60);
		ad2.showAd();
	}
}

//classified ad class
class ClassifiedAd
{
	//variables
	public string Category{ get; set; }
	public int Words{ get; set; }
	public double Price{get {return Words * 0.09;}}

	//object
	public ClassifiedAd(string category, int words)
	{
		Category = category;
		Words = words;
	}

	public void showAd()
	{
		WriteLine($"The classified ad with {Words} words in category {Category} costs {Price.ToString("C", CultureInfo.GetCultureInfo("en-US"))}");
	}


}