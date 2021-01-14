using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task_2_1
{
    public class Product
    {
        public string Id { get; }
        public string Brand { get; }
        public string Model { get; }
        public decimal Price { get; }
        public Product(string id, string brand, string model, decimal price)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Price = price;
        }
        public Product(string[] args) : this(args[0], args[1], args[2], Decimal.Parse(args[3])) { }
        public override string ToString()
        {
            return Id + " - " + Brand + " - " + Model + " - $" + Price;
        }
    }

    public class Remainder
    {
        public string ProductId { get; }
        public string Location { get; }
        public int RemainingAmount { get; }
        public Remainder(string productId, string location, int remainingAmount)
        {
            ProductId = productId;
            Location = location;
            RemainingAmount = remainingAmount;
        }
        public Remainder(string[] args) : this(args[0], args[1], int.Parse(args[2])) { }
    }

    public class Tag
    {
        public string ProductId { get; }
        public string TagValue { get; }
        public Tag(string productId, string tagValue)
        {
            ProductId = productId;
            TagValue = tagValue;
        }
        public Tag(string[] args) : this(args[0], args[1]) { }
    }
    class Program
    {
        private static List<Product> Products;
        private static List<Tag> Tags;
        private static List<Remainder> Inventory;
        static int Main(string[] args)
        {
            Console.WriteLine("ERP Reports Bot, made by Denys Sakadel");
            Console.WriteLine("Enter pls command");
            try
            {
                Products = File.ReadLines("Products.csv").Skip(1).Select(s => new Product(s.Split(';'))).ToList();
                Tags = File.ReadLines("Tags.csv").Skip(1).Select(s => new Tag(s.Split(';'))).ToList();
                Inventory = File.ReadLines("Inventory.csv").Skip(1).Select(s => new Remainder(s.Split(';'))).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong ");
                return -1;
            }
            

            while (true)
            {
                string command = "";
                Console.WriteLine("1. Exit");
                Console.WriteLine("2. Goods");
                Console.WriteLine("3. Remainders");
                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        System.Environment.Exit(0);
                        break;
                    case "2":
                        remainders();
                        break;
                    case "3":
                        goods();
                        break;
                    default:
                        Console.WriteLine("You entered wrong number");
                        break;
                }
            }

            return 0;
        }

        private static void goods()
        {

            while (true)
            {
                string command = "";
                Console.WriteLine("1. Return to general menu");
                Console.WriteLine("2. Search a product");
                Console.WriteLine("3. Sorted list by price by ascending");
                Console.WriteLine("4. Sorted list by price by descending");
                command = Console.ReadLine();
                int k = 0;
                switch (command)
                {
                    case "1":
                        return;
                    case "2":
                        searching_product();
                        break;
                    case "3":
                        var order = Products.OrderBy(x => x.Price).ToList();
                        foreach (var product in order) Console.WriteLine($"N{++k} {product}");
                        break;
                    case "4":
                        order = Products.OrderByDescending(x => x.Price).ToList();
                        foreach (var product in order) Console.WriteLine($"N{++k} {product}");
                        break;
                    default: Console.WriteLine("You entered wrong number");
                        break;
                }
            }
        }

        private static void searching_product()
        {
            Console.WriteLine("Enter searching product =>");
            var s = Console.ReadLine().ToLower();
            var list = new List<string>();
            Products.Where(x => x.Id.ToLower() == s).ToList().ForEach(p =>
                list.Add($"{p} [{String.Join(", ", Tags.Where(t => t.ProductId == p.Id).Select(t => t.TagValue))}]"));

            Products.Where(p => p.Brand.ToLower().Contains(s) || p.Model.ToLower().Contains(s)).ToList()
                .ForEach(p => list.Add($"{p} [{String.Join(", ", Tags.Where(t => t.ProductId == p.Id).Select(t => t.TagValue))}]"));

            Tags.Where(t => t.TagValue.ToLower().Contains(s)).ToList().ForEach(t => 
            Products.Where(x => x.Id == t.ProductId).ToList().ForEach(p =>
            list.Add($"{p} [{String.Join(", ", Tags.Where(t => t.ProductId == p.Id).Select(t => t.TagValue))}]")));

            list = list.Distinct().ToList();

            if (list.Count == 0) Console.WriteLine("Results didn't found");
            
            for (int i = 0; i < list.Count; i++) Console.WriteLine($"#{i + 1} {list[i]}");
            
        }
        private static void remainders()
        {
            while (true)
            {
                Console.WriteLine("1. Return to general menu");
                Console.WriteLine("2. Unavailable goods");
                Console.WriteLine("3. Remainders by ascending");
                Console.WriteLine("4. Remainders by descending");
                Console.WriteLine("5. Remainders by ID");
                string command = Console.ReadLine();
                int k = 0;
                switch (command)
                {
                    case "1":
                        return;
                    case "2":
                        var unnavalible = Products.Where(p => Inventory.All(x => x.ProductId != p.Id || x.RemainingAmount == 0))
                            .OrderBy(i => i.Id).ToList();
                        for (int i = 0; i < unnavalible.Count; i++) Console.WriteLine($"#{i + 1} {unnavalible[i]}");
                        break;

                    case "3":
                        var remainders = Products.Select(p => new
                        {
                            product = $"{p} [{String.Join(", ", Tags.Where(t => t.ProductId == p.Id).Select(t => t.TagValue))}]",
                            amount = Inventory.Where(x => x.ProductId == p.Id).Select(x => x.RemainingAmount).Sum()
                        }).ToList();

                        remainders = remainders.OrderBy(x => x.amount).ToList();
                        
                        foreach (var x in remainders) Console.WriteLine($"#{++k} {x.product}, {x.amount}");
                        break;

                    case "4":
                        var remaindersDesc = Products.Select(p => new
                        {
                            product = $"{p} [{String.Join(", ", Tags.Where(t => t.ProductId == p.Id).Select(t => t.TagValue))}]",
                            amount = Inventory.Where(x => x.ProductId == p.Id).Select(x => x.RemainingAmount).Sum()
                        }).ToList();
                        remaindersDesc = remaindersDesc.OrderByDescending(x => x.amount).ToList();
                        k = 0;
                        foreach (var x in remaindersDesc) Console.WriteLine($"#{++k} {x.product}, {x.amount}");
                        break;

                    case "5":
                        Console.WriteLine("Enter product id =>");
                        string id = Console.ReadLine().ToLower();
                        var report = Inventory.Where(x => x.ProductId.ToLower() == id)
                            .Select(x => new
                            {
                                location = x.Location,
                                amount = x.RemainingAmount
                            }).OrderByDescending(x => x.amount).ToList();
                        if (report.Count == 0) Console.WriteLine("Results didn't found");
                        for (int i = 0; i < report.Count; i++) Console.WriteLine($"#{i + 1} Location: {report[i].location}; Amount: {report[i].amount}");

                        break;

                    default:
                        Console.WriteLine("You entered wrong number");
                        break;
                }
            }
        }
        
    }
}
