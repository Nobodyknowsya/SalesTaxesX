using System;
using NUnit.Framework;
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
        public void CalculateTaxes_BookItem_NoTaxesCalculated()
        {
            //Setup 
            var book = new Item
            {
                Price = 12.49m,
                TypeOfItem = TypeOfItem.Book
            };

            //SUT Call
            var result = taxesCalculator.CalculateTaxes(book);

            //Assertion
            Assert.AreEqual(book.Price, result);
        }

        [Test]
        public void CalculateTaxes_MusicCd_TaxesAddedToThePrice()
        {
            //Setup 
            var musicCd = new Item
            {
                Price = 14.99m,
                TypeOfItem = TypeOfItem.Other
            };

            //SUT Call
            var result = taxesCalculator.CalculateTaxes(musicCd);

            //Assertion
            Assert.AreEqual(16.49m, result);
        }

        [Test]
        public void CalculateTaxes_ChocolateBar_NoTaxesCalculated()
        {
            //Setup
            var chocolateBar = new Item()
            {
                Price = 0.85m,
                TypeOfItem = TypeOfItem.Food
            };

            //SUT Call
            var result = taxesCalculator.CalculateTaxes(chocolateBar);

            //Assertion
            Assert.AreEqual(chocolateBar.Price, result);
        }

        [Test]
        public void CalculateTaxes_HeadachePills_NoTaxesCalculated()
        {
            //Setup
            var headachePills = new Item()
            {
                Price = 9.75m,
                TypeOfItem = TypeOfItem.Medical
            };

            //SUT Call
            var result = taxesCalculator.CalculateTaxes(headachePills);

            //Assertion
            Assert.AreEqual(headachePills.Price,result);
        }
    }


}
