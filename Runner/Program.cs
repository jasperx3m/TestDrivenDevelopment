using System;
using System.Collections.Generic;
using InventorySystem;
using Models;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Product product1 = new Product { ProductID = 1001, ProductName = "Chair", Quantity = 22 };
            Product product2 = new Product { ProductID = 1002, ProductName = "Tabler", Quantity = 15 };
            List<Product> productlist = new List<Product>();
            ProductFunction.AddProduct(productlist, product1);
            ProductFunction.AddProduct(productlist, product2);
            Console.WriteLine("Please choose a function \n[1]Add \n[2]Remove \n[3]Edit \n[View]");
            int function = Int32.Parse(Console.ReadLine());
            if (function==1)
            {
                Console.WriteLine("Enter ID, Product Name, and Quantity respectively");
                int id = Int32.Parse(Console.ReadLine());
                string name = Console.ReadLine();
                int qty = Int32.Parse(Console.ReadLine());
                Product newproduct = new Product { ProductID = id, ProductName = name, Quantity = qty };
                ProductFunction.AddProduct(productlist, newproduct);
                foreach (var i in productlist)
                {
                    Console.WriteLine("ID: {0} Name: {1} Quantity: {2}", i.ProductID, i.ProductName, i.Quantity);
                }
            }
        }
    }
}
