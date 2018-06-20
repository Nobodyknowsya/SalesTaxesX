namespace SalesTaxes.Model.Entities
{
    public class Item
    {
        public TypeOfItem TypeOfItem { get; set; }
        public decimal Price { get; set; }
        public bool IsImported { get; set; }
    }
}