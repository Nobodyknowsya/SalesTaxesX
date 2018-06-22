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
    public class BasketItemManagerFixture
    {
        private BasketItemManager _basketItemManager;
        private Mock<ITaxesCalculator> taxCalculatorMock;
        
        [SetUp]
        public void Setup()
        {
            taxCalculatorMock=new Mock<ITaxesCalculator>();
            _basketItemManager = new BasketItemManager(taxCalculatorMock.Object);
        }

        [Test]
        public void Ctor_TaxCalculatorIsNull_ThrowsException()
        {
            try
            {
                new BasketItemManager(null);
                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
                
            }
        }

        [Test]
        public void AddToBasket_WithNoTaxes_PriceIsEqualThanItemPrice()
        {
            //Setup
            var book = new Item()
            {
                Price = 12.49m,
                TypeOfItem = TypeOfItem.Book,
                Description = "Book"
            };

            //SUT Call
            var purchasedItem = _basketItemManager.AddToBasket(book);

            //Assertion
            Assert.AreEqual(book.Price,purchasedItem.FinalPrice);
            Assert.AreEqual(0,purchasedItem.SaleTax);
            Assert.AreEqual(book.Description,purchasedItem.Description);
        }

        [Test]
        public void AddToBasket_WithTaxes_FinalPriceIsPricePlusTaxes()
        {
            //Setup
            var musicCd = new Item()
            {
                Price = 14.99m,
                TypeOfItem = TypeOfItem.Other,
                Description = "Music CD"
            };

            taxCalculatorMock.Setup(s => s.CalculateItemTaxes(It.IsAny<Item>())).Returns(() => 1.5m);
            
            //SUT Call
            var purchasedItem = _basketItemManager.AddToBasket(musicCd);

            //Assertion
            Assert.AreEqual(16.49m, purchasedItem.FinalPrice);
            Assert.AreEqual(1.5m, purchasedItem.SaleTax);
            taxCalculatorMock.Verify(s => s.CalculateItemTaxes(It.IsAny<Item>()),Times.Exactly(1));
        }
    }
}