using System;
using System.Collections.Generic;

namespace Task_1_3
{
    public interface IRegion
    {
        public string Brand { get; }
        public string Country { get; }
    }

    public interface IRegionSettings
    {
        public string WebSite { get; }
    }

    public class Region : IRegion
    {
        public string Brand { get; }
        public string Country { get; }

        public Region(string brand, string country)
        {
            Brand = brand;
            Country = country;
        }
        public override int GetHashCode()
        {
            return Brand.GetHashCode() ^ Country.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return Brand.Equals(((Region)obj)?.Brand) && Country.Equals(((Region)obj)?.Country);
        }

        public override string ToString() => Brand + " " + Country;
    }
    public class RegionSettings : IRegionSettings
    {
        public string WebSite { get; }
        public RegionSettings(string webSite)
        {
            WebSite = webSite;
        }
        public override string ToString() => WebSite;
    }

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Dictionary whith key, made by Denys Sakadel");
            string stop = "";
            while (stop != "stop")
            {
                int n = 0;
                M: Console.WriteLine("Enter number of elements");
                try
                {
                    n = int.Parse(Console.ReadLine());
                }
                catch (Exception) 
                { 
                    Console.WriteLine("You entered wrong expression, pls enter number of elements");
                    goto M;
                }

                Dictionary<Region, RegionSettings> region = new Dictionary<Region, RegionSettings>();
                for (int i = 0; i < n; i++)
                {
                    N: Console.WriteLine("Enter brand =>");
                    string brand = Console.ReadLine();
                    Console.WriteLine("Enter country =>");
                    string country = Console.ReadLine();
                    Console.WriteLine("Enter website =>");
                    string website = Console.ReadLine();
                    try
                    {
                        region.Add(new Region(brand, country), new RegionSettings(website));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("You entered a duplicate, pls try again");
                        goto N;
                    }
                }

                Console.WriteLine("Region => ");
                foreach (var r in region)
                {
                    Console.WriteLine("[ " + r.Key + " ] = [ " + r.Value + " ]");
                }


                Console.WriteLine("Exit => enter stop");
                stop = Console.ReadLine();
            } 

        }

        
    }
}
