namespace SalesTaxes.Model.Entities
{
    public class Receipt
    {
        public decimal SalesTaxes { get; set; }
        public decimal Total { get; set; }
        public PurchasedItem[] PurchasedItems { get; set; }
        public ReceiptDetail[] Details { get; set; }
    }

    public class ReceiptDetail
    {
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}