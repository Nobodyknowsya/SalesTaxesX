using NUnit.Framework;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services;

namespace SalesTaxes.Fixtures
{
    [TestFixture]
    public class ReceiptBuilderFixtures
    {
        private ReceiptBuilder receiptBuilder;

        [SetUp]
        public void SetUp()
        {
            receiptBuilder=new ReceiptBuilder();
        }

        [Test]
        public void CreateReceipt_WithThreeItems()
        {
            //Setup
            var book = new PurchasedItem()
            {
                Description = "A very good book",
                Price = 12.49m,
                SaleTax = 0
            };

            var musicCd = new PurchasedItem()
            {
                Description = "An awful cd",
                Price = 16.49m,
                SaleTax = 1.5m
            };

            var chocolate = new PurchasedItem()
            {
                Description = "A delicious brownie",
                Price = 0.85m,
                SaleTax = 0
            };

            var cart = new PurchasedItem[] {book,musicCd,chocolate};

            //SUT Call
            var receipt = receiptBuilder.CreateReceipt(cart);

            //Assertion
            Assert.AreEqual(1.5,receipt.SalesTaxes);
            Assert.AreEqual(29.83m, receipt.Total);
            Assert.AreEqual(3, receipt.PurchasedItems.Length);
            Assert.AreEqual(book, receipt.PurchasedItems[0]);
            Assert.AreEqual(musicCd, receipt.PurchasedItems[1]);
            Assert.AreEqual(chocolate, receipt.PurchasedItems[2]);
        }
    }
}