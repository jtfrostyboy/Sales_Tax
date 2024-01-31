using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        
        string filePath = "C:\\Users\\Jthom\\github\\Sales_Tax\\receipt1.txt";

        List<Item> items = ReadInputFromFile(filePath);

        Console.WriteLine("\nOUTPUT:");

        decimal totalSalesTaxes = 0;
        decimal totalSalesPrice = 0;

        foreach (var item in items)
        {
            item.CalculateTax();
            totalSalesTaxes += item.SalesTax;
            totalSalesPrice += item.FinalPrice;

            Console.WriteLine($"{item.Name}: {item.FinalPrice:F2}" + (item.Quantity > 1 ? $" ({item.Quantity} @ {item.Price:F2} each)" : ""));
        }

        Console.WriteLine($"Sales Taxes: {totalSalesTaxes:F2}");
        Console.WriteLine($"Total: {totalSalesPrice:F2}");
    }

    static List<Item> ReadInputFromFile(string filePath)
    {
        List<Item> items = new List<Item>();

        try
        {
            foreach (string line in File.ReadLines(filePath))
            {
                if (line.ToLower() == "done")
                    break;

                string[] parts = line.Split(' ');

                int quantity;
                if (!int.TryParse(parts[0], out quantity))
                {
                    Console.WriteLine("Invalid quantity in the file. Please check the format.");
                    continue;
                }

                decimal itemPrice;
                if (!decimal.TryParse(parts[parts.Length - 1], out itemPrice))
                {
                    Console.WriteLine("Invalid price in the file. Please check the format.");
                    continue;
                }

                string itemName = string.Join(" ", parts[1..^2]);
                bool isImported = itemName.Contains("imported", StringComparison.OrdinalIgnoreCase);
                bool isExempt = itemName.Contains("book", StringComparison.OrdinalIgnoreCase)
                                || itemName.Contains("chocolate", StringComparison.OrdinalIgnoreCase)
                                || itemName.Contains("pill", StringComparison.OrdinalIgnoreCase);

                Item newItem = new Item(itemName, itemPrice, isImported, isExempt)
                {
                    Quantity = quantity
                };
                items.Add(newItem);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }

        return items;
    }
}