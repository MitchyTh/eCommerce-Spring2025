// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product?> list = ProductServiceProxy.Current.Products;
            Cart cart = new Cart();

            Console.WriteLine("Welcome to the store!");
            char choice;
            char MenuChoice;
            do
            {
                Console.WriteLine("Inventory or Cart?");
                Console.WriteLine("I. Inventory");
                Console.WriteLine("C. Cart");
                Console.WriteLine("Q. Checkout");

                string? input = Console.ReadLine().ToUpper();
                MenuChoice = input[0];

                if (MenuChoice == 'I')
                    do
                    {
                        Console.WriteLine("C. Create new inventory item");
                        Console.WriteLine("R. Read all inventory items");
                        Console.WriteLine("U. Update an inventory item");
                        Console.WriteLine("D. Delete an inventory item");
                        Console.WriteLine("Q. Exit");

                        input = Console.ReadLine().ToUpper();
                        choice = input[0];
                        switch (choice)
                        {
                            case 'C':
                                Console.WriteLine("How many?");
                                string? tempAmount = Console.ReadLine();
                                if (tempAmount == null)
                                {
                                    break;
                                }
                                int amount = int.Parse(tempAmount);
                                Console.WriteLine("Product name:");
                                if (amount > 0)
                                {
                                    ProductServiceProxy.Current.AddorUpdate(new Product
                                    {
                                        Name = Console.ReadLine(),
                                        //Generates a random number between 5 - 100 for the price of a product
                                        Price = Math.Round((decimal)(new Random().NextDouble() * (100 - 5) + 5), 2),
                                        Quantity = amount
                                    });
                                }
                                break;
                            case 'R':
                                list.ForEach(Console.WriteLine);
                                break;
                            case 'U':
                                Console.WriteLine("Which product would you like to update?");
                                int selection = int.Parse(Console.ReadLine() ?? "-1");
                                var selectedProd = list.FirstOrDefault(p => p?.Id == selection);
                                Console.WriteLine("New name:");
                                if (selectedProd != null)
                                {
                                    selectedProd.Name = Console.ReadLine() ?? "ERROR";
                                    ProductServiceProxy.Current.AddorUpdate(selectedProd);
                                }
                                break;
                            case 'D':
                                Console.WriteLine("Which product would you like to delete?");
                                selection = int.Parse(Console.ReadLine() ?? "-1");
                                ProductServiceProxy.Current.Delete(selection);
                                break;
                            case 'Q':
                                break;
                        }
                    } while (choice != 'Q');
                else if (MenuChoice == 'C')
                {
                    do
                    {
                        Console.WriteLine("C. Add an item to the cart");
                        Console.WriteLine("R. Read all cart items");
                        Console.WriteLine("U. Update an cart quantity");
                        Console.WriteLine("D. Delete an cart item");
                        Console.WriteLine("Q. Exit");

                        input = Console.ReadLine().ToUpper();
                        choice = input[0];
                        switch (choice)
                        {
                            case 'C':
                                list.ForEach(Console.WriteLine);
                                Console.WriteLine("\nWhich product would you like to add to the cart?");
                                int selection = int.Parse(Console.ReadLine() ?? "-1");
                                var selectedProd = list.FirstOrDefault(p => p?.Id == selection);
                                if (selectedProd == null)
                                {
                                    break;
                                }
                                Console.WriteLine("How many?");
                                int amount = int.Parse(Console.ReadLine() ?? "-1");
                                if (selectedProd.Quantity >= amount && amount > 0)
                                {
                                    cart.Add(new Product
                                    {
                                        Id = selectedProd.Id,
                                        Name = selectedProd.Name,
                                        Price = selectedProd.Price,
                                        Quantity = amount
                                    });
                                    ProductServiceProxy.Current.ChangeItemQuantity(selection, amount);
                                }
                                break;
                            case 'R':
                                cart.Items.ForEach(Console.WriteLine);
                                break;
                            case 'U':
                                Console.WriteLine("Which product ID would you like to update?");
                                selection = int.Parse(Console.ReadLine() ?? "-1");
                                selectedProd = cart.Items.FirstOrDefault(p => p?.Id == selection);
                                if (selectedProd != null)
                                {
                                    Console.WriteLine("How many would you like instead?");
                                    amount = int.Parse(Console.ReadLine());

                                    //If the amount is less than the total amount of this product
                                    if (amount < selectedProd.Quantity && amount > 0)
                                    {
                                        ProductServiceProxy.Current.AddUpdateCart(selection, selectedProd.Quantity - amount, selectedProd);
                                        selectedProd.Quantity = amount;
                                    }
                                    else if (amount > selectedProd.Quantity)
                                    {
                                        bool update = ProductServiceProxy.Current.RemoveUpdateCart(selection, amount - selectedProd.Quantity);
                                        if (update)
                                        {
                                            selectedProd.Quantity = amount;
                                        }
                                    }
                                }
                                break;
                            case 'D':
                                Console.WriteLine("Which product would you like to remove from the cart?");
                                selection = int.Parse(Console.ReadLine() ?? "-1");
                                cart.Delete(selection);
                                break;
                            case 'Q':
                                break;
                        }
                    } while (choice != 'Q');
                }

            } while (MenuChoice != 'Q');

            Decimal total = 0;
            foreach (var item in cart.Items)
            {
                total += item.Price * item.Quantity;
            }
            total = total + (total * 0.07m);
            total = Math.Round(total, 2);

            Console.WriteLine($"Your total is ${total}");

        }
    }
}