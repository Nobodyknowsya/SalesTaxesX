namespace SalesTaxes.Model.Entities
{
    public class PurchasedItem
    {
        public decimal Price { get; set; }
        public decimal SaleTax { get; set; }
        public string Description { get; set; }
    }
}