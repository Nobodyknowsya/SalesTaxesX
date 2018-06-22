using NUnit.Framework;
using SalesTaxes.Data;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services;

namespace SalesTaxes.Fixtures
{
    [TestFixture]
    public class IntegrationTests
    {
        private ShoppingBasketService shoppingBasketService;

        [SetUp]
        public void Setup()
        {
            var taxRateRetreiver = new TaxRateRetriever();
            var taxesCalculator = new TaxesCalculator(taxRateRetreiver);
            var basketItemManager = new BasketItemManager(taxesCalculator);
            var receiptBuilder = new ReceiptBuilder();
            shoppingBasketService = new ShoppingBasketService(receiptBuilder, basketItemManager);
        }

        [Test]
        public void Input1()
        {
            //Setup
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

            var items = new Item[] { book, musicCd, chocolateBar };

            //SUT Call
            Receipt receipt = shoppingBasketService.Checkout(items);

            //Assertion
            Assert.AreEqual(1,receipt.Details[0].Quantity);
            Assert.AreEqual(book.Description,receipt.Details[0].Description);
            Assert.AreEqual(book.Price,receipt.Details[0].Price);
            Assert.AreEqual(1,receipt.Details[1].Quantity);
            Assert.AreEqual(musicCd.Description,receipt.Details[1].Description);
            Assert.AreEqual(16.49, receipt.Details[1].Price);
            Assert.AreEqual(1,receipt.Details[2].Quantity);
            Assert.AreEqual(chocolateBar.Description,receipt.Details[2].Description);
            Assert.AreEqual(chocolateBar.Price,receipt.Details[2].Price);
            Assert.AreEqual(1.5m, receipt.SalesTaxes);
            Assert.AreEqual(29.83m, receipt.Total);
        }

        [Test]
        public void Input2()
        {
            //Setup
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


            var items = new Item[] { importedBoxOfChocolate, importedBottleOfPerfume};



            //SUT Call
            Receipt receipt = shoppingBasketService.Checkout(items);

            //Assertion
            Assert.AreEqual(1, receipt.Details[0].Quantity);
            Assert.AreEqual(importedBoxOfChocolate.Description, receipt.Details[0].Description);
            Assert.AreEqual(10.50m, receipt.Details[0].Price);
            Assert.AreEqual(1, receipt.Details[1].Quantity);
            Assert.AreEqual(importedBottleOfPerfume.Description, receipt.Details[1].Description);
            Assert.AreEqual(54.65m, receipt.Details[1].Price);
            Assert.AreEqual(7.65m, receipt.SalesTaxes);
            Assert.AreEqual(65.15m, receipt.Total);

        }

        [Test]
        public void Input3()
        {
            //Setup
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

            var items = new Item[] { importedBottleOfPerfume, bottleOfPerfume, packetOfHeadachePills, importedBoxOfChocolate };

            //SUT Call
            Receipt receipt = shoppingBasketService.Checkout(items);

            //Assertion
            Assert.AreEqual(1, receipt.Details[0].Quantity);
            Assert.AreEqual(importedBottleOfPerfume.Description, receipt.Details[0].Description);
            Assert.AreEqual(32.19m, receipt.Details[0].Price);
            Assert.AreEqual(1, receipt.Details[1].Quantity);
            Assert.AreEqual(bottleOfPerfume.Description, receipt.Details[1].Description);
            Assert.AreEqual(20.89m, receipt.Details[1].Price);
            Assert.AreEqual(1, receipt.Details[2].Quantity);
            Assert.AreEqual(packetOfHeadachePills.Description, receipt.Details[2].Description);
            Assert.AreEqual(9.75, receipt.Details[2].Price);
            Assert.AreEqual(1, receipt.Details[3].Quantity);
            Assert.AreEqual(importedBoxOfChocolate.Description, receipt.Details[3].Description);
            Assert.AreEqual(11.85, receipt.Details[3].Price);
            Assert.AreEqual(6.70m, receipt.SalesTaxes);
            Assert.AreEqual(74.68m, receipt.Total);
        }
    }
}