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
            receiptBuilder = new ReceiptBuilder();
        }

        [Test]
        public void CreateReceipt_WithThreeItems()
        {
            //Setup
            var book = new PurchasedItem()
            {
                Description = "A very good book",
                FinalPrice = 12.49m,
                SaleTax = 0
            };

            var musicCd = new PurchasedItem()
            {
                Description = "An awful cd",
                FinalPrice = 16.49m,
                SaleTax = 1.5m
            };

            var chocolate = new PurchasedItem()
            {
                Description = "A delicious brownie",
                FinalPrice = 0.85m,
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

        [Test]
        public void CreateReceipt_TwoEqualItems_JustOneDetail()
        {
            //Setup
            var itemDescription = "A very good book";
            var firstImportBook= new PurchasedItem()
            {
                Description =itemDescription,
                FinalPrice = 12.49m,
                SaleTax = 0.6m
            };

            var sevondImportBook = new PurchasedItem()
            {
                Description = itemDescription,
                FinalPrice = 12.49m,
                SaleTax = 0.6m
            };

            var cart = new PurchasedItem[] { firstImportBook, sevondImportBook };

            //SUT Call
            var receipt = receiptBuilder.CreateReceipt(cart);

            //Assertiom
            Assert.AreEqual(1,receipt.Details.Length);
            Assert.AreEqual(2,receipt.Details[0].Quantity);
            Assert.AreEqual("A very good book", receipt.Details[0].Description);
            Assert.AreEqual(24.98m, receipt.Details[0].Price);
            Assert.AreEqual(2,receipt.PurchasedItems.Length);
        }

        [Test]
        public void CreateReceipt_TwoItemsDifferentDescription_TwoDetail()
        {
            //Setup
            var firstItemDescription = "a very good book";
            var secondItemDescription = "A very good book";

            var firstImportBook = new PurchasedItem()
            {
                Description = firstItemDescription,
                FinalPrice = 12.49m,
                SaleTax = 0.6m
            };

            var sevondImportBook = new PurchasedItem()
            {
                Description = secondItemDescription,
                FinalPrice = 12.49m,
                SaleTax = 0.6m
            };

            var cart = new PurchasedItem[] { firstImportBook, sevondImportBook };

            //SUT Call
            var receipt = receiptBuilder.CreateReceipt(cart);

            //Assertiom
            Assert.AreEqual(2, receipt.Details.Length);
            Assert.AreEqual(1, receipt.Details[0].Quantity);
            Assert.AreEqual(1, receipt.Details[1].Quantity);
            Assert.AreEqual(firstItemDescription, receipt.Details[0].Description);
            Assert.AreEqual(secondItemDescription, receipt.Details[1].Description);
            Assert.AreEqual(12.49m, receipt.Details[0].Price);
            Assert.AreEqual(12.49m, receipt.Details[0].Price);
            Assert.AreEqual(2, receipt.PurchasedItems.Length);
        }

        [Test]
        public void CreateReceipt_TwoItemsDifferentFinalPrice_TwoDetail()
        {
            //Setup
            var iItemDescription = "A very good book";
            var firstFinalPrice = 12.49m;
            var secondFinalPrice = 112.49m;

            var firstImportBook = new PurchasedItem()
            {
                Description = iItemDescription,
                FinalPrice = firstFinalPrice,
                SaleTax = 0.6m
            };

            var sevondImportBook = new PurchasedItem()
            {
                Description = iItemDescription,
                FinalPrice = secondFinalPrice,
                SaleTax = 0.6m
            };

            var cart = new PurchasedItem[] { firstImportBook, sevondImportBook };

            //SUT Call
            var receipt = receiptBuilder.CreateReceipt(cart);

            //Assertiom
            Assert.AreEqual(2, receipt.Details.Length);
            Assert.AreEqual(1, receipt.Details[0].Quantity);
            Assert.AreEqual(1, receipt.Details[1].Quantity);
        }

        [Test]
        public void CreateReceipt_TwoItemsDifferentSaleTax_TwoDetail()
        {
            //Setup
            var iItemDescription = "A very good book";
            var firstSaleTax = 1m;
            var secondSaleTax = 2m;

            var firstImportBook = new PurchasedItem()
            {
                Description = iItemDescription,
                FinalPrice = 12.49m,
                SaleTax = firstSaleTax
            };

            var sevondImportBook = new PurchasedItem()
            {
                Description = iItemDescription,
                FinalPrice = 12.49m,
                SaleTax = secondSaleTax
            };

            var cart = new PurchasedItem[] { firstImportBook, sevondImportBook };

            //SUT Call
            var receipt = receiptBuilder.CreateReceipt(cart);

            //Assertiom
            Assert.AreEqual(2, receipt.Details.Length);
            Assert.AreEqual(1, receipt.Details[0].Quantity);
            Assert.AreEqual(1, receipt.Details[1].Quantity);
        }
    }
}