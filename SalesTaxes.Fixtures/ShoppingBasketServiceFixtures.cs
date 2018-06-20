using System.Linq;
using Moq;
using NUnit.Framework;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Fixtures
{
    [TestFixture]
    public class ShoppingBasketServiceFixtures
    {
        private Mock<IReceiptBuilder> receiptBuilderMock;
        private Mock<IBasketItemManager> basketItemManager;

        [SetUp]
        public void SetUp()
        {
            receiptBuilderMock = new Mock<IReceiptBuilder>();
            basketItemManager = new Mock<IBasketItemManager>();
        }

        [Test]
        public void Checkout()
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

            basketItemManager.Setup(m => m.AddToBasket(It.IsAny<Item>())).Returns( new PurchasedItem());
            receiptBuilderMock.Setup(m => m.CreateReceipt(It.IsAny<PurchasedItem[]>())).Returns( new Receipt());
            
            var shoppingBasketService = new ShoppingBasketService(receiptBuilderMock.Object, basketItemManager.Object);

            //SUT Call
            Receipt receipt = shoppingBasketService.Checkout(items);

            //Assertion
            basketItemManager.Verify(m=>m.AddToBasket(It.IsAny<Item>()),Times.Exactly(3));
            receiptBuilderMock.Verify(m=>m.CreateReceipt(It.IsAny<PurchasedItem[]>()),Times.Exactly(1));
            
        }
    }
}