namespace SalesTaxes.Model.Entities
{
    public class PurchasedItem
    {
        public decimal FinalPrice { get; set; }
        public decimal SaleTax { get; set; }
        public string Description { get; set; }
    }
}