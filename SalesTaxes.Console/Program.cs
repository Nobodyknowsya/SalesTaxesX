using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesTaxes.Data;
using Unity;
using SalesTaxes.Model;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var unityContainer = RegisterTypes();
            var shoppingBasketService = unityContainer.Resolve<IShoppingBasketService>();

            var input1 = CreateItemsInput1();
            var receipt1 = shoppingBasketService.Checkout(input1);
            DisplayReceipt(receipt1);

            var input2 = CreateItemsInput2();
            var receipt2 = shoppingBasketService.Checkout(input2);
            DisplayReceipt(receipt2);

            var input3 = CreateItemsInput3();
            var receipt3 = shoppingBasketService.Checkout(input3);
            DisplayReceipt(receipt3);

            var multipleQuantity = CareateMultipleQuantityInput();
            var receiptMultipleQuantity = shoppingBasketService.Checkout(multipleQuantity);
            DisplayReceipt(receiptMultipleQuantity);
            System.Console.ReadLine();
        }

        private static void DisplayReceipt(Receipt receipt)
        {
            var headerQ = "QUANTITY";
            var headerD = "DESCRIPTION";
            var headerP = "PRICE";
            System.Console.WriteLine("{0}\t{1}\t{2}", headerQ.PadRight(10), headerD.PadRight(25), headerP);

            foreach (var detail in receipt.Details)
            {
                var detailRow = string.Format(
                    "{0}\t{1}\t{2}",
                    detail.Quantity.ToString().PadRight(15),
                    detail.Description.PadRight(30),
                    detail.Price);

                System.Console.WriteLine(detailRow);
            }

            System.Console.WriteLine("SALES TAXES:\t" + receipt.SalesTaxes);
            System.Console.WriteLine("TOTAL:\t\t"+receipt.Total);
            System.Console.WriteLine("-------------------------------------------------------------");
        }

        private static Item[] CareateMultipleQuantityInput()
        {
            var itemsList = new List<Item>();
            itemsList.AddRange(CreateItemsInput1());
            itemsList.AddRange(CreateItemsInput1());
            itemsList.AddRange(CreateItemsInput2());
            itemsList.AddRange(CreateItemsInput2());
            itemsList.AddRange(CreateItemsInput3());
            itemsList.AddRange(CreateItemsInput3());

            return itemsList.ToArray();
        }

        private static Item[] CreateItemsInput1()
        {
            var book = new Item()
            {
                Description = "book",
                Price = 12.49m,
                TypeOfItem = TypeOfItem.Book,
                IsImported = false
            };

            var musicCd = new Item()
            {
                Description = "music cd",
                Price = 14.99m,
                TypeOfItem = TypeOfItem.Other,
                IsImported = false
            };

            var chocolateBar = new Item()
            {
                Description = "chocolate bar",
                Price = 0.85m,
                TypeOfItem = TypeOfItem.Food,
                IsImported = false
            };

            return new Item[] { book, musicCd, chocolateBar };
        }

        private static Item[] CreateItemsInput2()
        {
            var importedBoxOfChocolate = new Item()
            {
                Description = "imported Box Of Chocolate",
                Price = 10.00m,
                TypeOfItem = TypeOfItem.Food,
                IsImported = true
            };

            var importedBottleOfPerfume = new Item()
            {
                Description = "Imported Bottle Of Perfume",
                Price = 47.50m,
                TypeOfItem = TypeOfItem.Other,
                IsImported = true
            };


            return new Item[] { importedBoxOfChocolate, importedBottleOfPerfume };

        }

        private static Item[] CreateItemsInput3()
        {
            var importedBottleOfPerfume = new Item()
            {
                Description = "imported Bottle Of Perfume",
                Price = 27.99m,
                TypeOfItem = TypeOfItem.Other,
                IsImported = true
            };

            var bottleOfPerfume = new Item()
            {
                Description = "Bottle Of Perfume",
                Price = 18.99m,
                TypeOfItem = TypeOfItem.Other,
                IsImported = false
            };

            var packetOfHeadachePills = new Item()
            {
                Description = "packet Of Headache Pills",
                Price = 9.75m,
                TypeOfItem = TypeOfItem.Medical,
                IsImported = false
            };

            var importedBoxOfChocolate = new Item()
            {
                Description = "imported Box Of Chocolate",
                Price = 11.25m,
                TypeOfItem = TypeOfItem.Food,
                IsImported = true
            };

            return new Item[] { importedBottleOfPerfume, bottleOfPerfume, packetOfHeadachePills, importedBoxOfChocolate };

        }

        private static UnityContainer RegisterTypes()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<ITaxRateRetriever, TaxRateRetriever>();
            unityContainer.RegisterType<ITaxesCalculator, TaxesCalculator>();
            unityContainer.RegisterType<IBasketItemManager, BasketItemManager>();
            unityContainer.RegisterType<IReceiptBuilder, ReceiptBuilder>();
            unityContainer.RegisterType<IShoppingBasketService, ShoppingBasketService>();

            return unityContainer;
        }
    }
}
