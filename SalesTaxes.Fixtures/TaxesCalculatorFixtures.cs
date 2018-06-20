using System;
using NUnit.Framework;
using SalesTaxes.Model;
using SalesTaxes.Model.Entities;

namespace SalesTaxes.Fixtures
{
    [TestFixture]
    public class TaxesCalculatorFixtures
    {
        private TaxesCalculator taxesCalculator;

        [SetUp]
        public void SetUp()
        {
            taxesCalculator = new TaxesCalculator();
        }

        [Test]
        public void CalculateTotalPrice_BookItem_NoTaxesCalculated()
        {
            //Setup 
            var book = new Item
            {
                Price = 12.49m,
                TypeOfItem = TypeOfItem.Book
            };

            //SUT Call
            var result = taxesCalculator.CalculateTotalPrice(book);

            //Assertion
            Assert.AreEqual(book.Price, result);
        }

        [Test]
        public void CalculateTotalPrice_MusicCd_TaxesAddedToThePrice()
        {
            //Setup 
            var musicCd = new Item
            {
                Price = 14.99m,
                TypeOfItem = TypeOfItem.Other
            };

            //SUT Call
            var result = taxesCalculator.CalculateTotalPrice(musicCd);

            //Assertion
            Assert.AreEqual(16.49m, result);
            //Assert.AreEqual(16.49m, result.TotalPrice);
            //Assert.AreEqual(1.50m, result.ItemTaxes);
        }

        [Test]
        public void CalculateTotalPrice_ChocolateBar_NoTaxesCalculated()
        {
            //Setup
            var chocolateBar = new Item()
            {
                Price = 0.85m,
                TypeOfItem = TypeOfItem.Food
            };

            //SUT Call
            var result = taxesCalculator.CalculateTotalPrice(chocolateBar);

            //Assertion
            Assert.AreEqual(chocolateBar.Price, result);
        }

        [Test]
        public void CalculateTotalPrice_HeadachePills_NoTaxesCalculated()
        {
            //Setup
            var headachePills = new Item()
            {
                Price = 9.75m,
                TypeOfItem = TypeOfItem.Medical
            };

            //SUT Call
            var result = taxesCalculator.CalculateTotalPrice(headachePills);

            //Assertion
            Assert.AreEqual(headachePills.Price,result);
        }

        [Test]
        public void CalculateItemTaxes_MusicCd_TaxesAreCalculated()
        {
            //Setup 
            var musicCd = new Item
            {
                Price = 14.99m,
                TypeOfItem = TypeOfItem.Other
            };

            //SUT Call
            var result = taxesCalculator.CalculateItemTaxes(musicCd);

            //Assertion
            Assert.AreEqual(1.50m, result);
        }

        [Test]
        public void CalculateItemTaxes_Book_NoTaxesAreCalculated()
        {
            //Setup 
            var book = new Item
            {
                Price = 12.49m,
                TypeOfItem = TypeOfItem.Book
            };

            //SUT Call
            var result = taxesCalculator.CalculateItemTaxes(book);

            //Assertion
            Assert.AreEqual(0,result);
        }

        [Test]
        public void CalculateItemTaxes_HeadachePills_NoTaxesAreCalculated()
        {
            //Setup
            var headachePills = new Item()
            {
                Price = 9.75m,
                TypeOfItem = TypeOfItem.Medical
            };

            //SUT Call
            var result = taxesCalculator.CalculateItemTaxes(headachePills);

            //Assertion
            Assert.AreEqual(0,result);
        }

        [Test]
        public void CalculateItemTaxes_ChocolateBar_NoTaxesAreCalculated()
        {
            //Setup
            var chocolateBar = new Item()
            {
                Price = 0.85m,
                TypeOfItem = TypeOfItem.Food
            };

            //SUT Call
            var result = taxesCalculator.CalculateItemTaxes(chocolateBar);

            //Assertion
            Assert.AreEqual(0,result);
        }

        [Test]
        public void CalculateItemTaxes_ImportedChocolate_ImportedItemTaxCalculated()
        {
            //Setup
            var importedChocolates = new Item()
            {
                Price = 10m,
                TypeOfItem = TypeOfItem.Food,
                IsImported = true
            };

            //SUT Call
            var result = taxesCalculator.CalculateItemTaxes(importedChocolates);

            //Assertion
            Assert.AreEqual(0.5m,result);
        }

        [Test]
        public void CalculateItemTaxes_ImportedPerfume_ImportedItemAndNoTaxFreeCalculated()
        {
            //Setup
            var importedPerfume = new Item()
            {
                Price = 47.5m,
                TypeOfItem = TypeOfItem.Other,
                IsImported = true
            };

            //SUT Call
            var result = taxesCalculator.CalculateItemTaxes(importedPerfume);

            //Assertion
            Assert.AreEqual(7.1m,result);
        }
    }


}
