namespace SalesTaxes.Model.Entities
{
    public class Receipt
    {
        public decimal SalesTaxes { get; set; }
        public decimal Total { get; set; }
        public PurchasedItem[] PurchasedItems { get; set; }
        public ReceiptDetail[] Details { get; set; }
    }
}