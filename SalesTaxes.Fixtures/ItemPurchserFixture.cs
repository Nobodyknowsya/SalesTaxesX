using Moq;
using NUnit.Framework;
using SalesTaxes.Model;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Fixtures
{
    [TestFixture]
    public class ItemPurchserFixture
    {
        private ItemPurchaser itemPurchaser;
        private Mock<ITaxesCalculator> taxCalculatorMock;
        
        [SetUp]
        public void Setup()
        {
            taxCalculatorMock=new Mock<ITaxesCalculator>();
            itemPurchaser = new ItemPurchaser(taxCalculatorMock.Object);
        }

        [Test]
        public void PurchaseItem_WithNoTaxes_PriceIsEqualThanItemPrice()
        {
            //Setup
            var book = new Item()
            {
                Price = 12.49m,
                TypeOfItem = TypeOfItem.Book,
                Description = "Book"
            };

            //SUT Call
            var purchasedItem = itemPurchaser.PurchaseItem(book);

            //Assertion
            Assert.AreEqual(book.Price,purchasedItem.FinalPrice);
            Assert.AreEqual(0,purchasedItem.SaleTax);
            Assert.AreEqual(book.Description,purchasedItem.Description);
        }

        [Test]
        public void PurchaseItem_WithTaxes_FinalPriceIsPricePlusTaxes()
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
            var purchasedItem = itemPurchaser.PurchaseItem(musicCd);

            //Assertion
            Assert.AreEqual(16.49m, purchasedItem.FinalPrice);
            Assert.AreEqual(1.5m, purchasedItem.SaleTax);
        }
    }
}