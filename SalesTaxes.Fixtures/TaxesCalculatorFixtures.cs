using System;
using Moq;
using NUnit.Framework;
using SalesTaxes.Model;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Fixtures
{
    [TestFixture]
    public class TaxesCalculatorFixtures
    {
        private TaxesCalculator taxesCalculator;
        private Mock<ITaxRateRetriever> taxRateRetriever;

        [SetUp]
        public void SetUp()
        {
            taxRateRetriever = new Mock<ITaxRateRetriever>();
            taxRateRetriever.Setup(s => s.GetTaxRate()).Returns(0.1m);
            taxRateRetriever.Setup(s => s.GetImportTaxRate()).Returns(0.05m);
            taxesCalculator = new TaxesCalculator(taxRateRetriever.Object);
        }

        [Test]
        public void Ctor_TaxRateRetrieverIsNull_ThrowsException()
        {
            try
            {
                taxesCalculator = new TaxesCalculator(null);
                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
            }
            
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
            taxRateRetriever.Verify(t=>t.GetTaxRate(),Times.Once);
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
            taxRateRetriever.Verify(t => t.GetTaxRate(), Times.Never);

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
            taxRateRetriever.Verify(t => t.GetTaxRate(), Times.Never);

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
            taxRateRetriever.Verify(t => t.GetTaxRate(), Times.Never);

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
            taxRateRetriever.Verify(t => t.GetTaxRate(), Times.Never);
            taxRateRetriever.Verify(t => t.GetImportTaxRate(), Times.Once);

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
            Assert.AreEqual(7.15m,result);
            taxRateRetriever.Verify(t => t.GetTaxRate(), Times.Once);
            taxRateRetriever.Verify(t => t.GetImportTaxRate(), Times.Once);
        }
    }
}
