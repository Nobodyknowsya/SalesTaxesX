using SalesTaxes.Model.Entities;

namespace SalesTaxes.Model.Services.Contracts
{
    public interface ITaxesCalculator
    {
        decimal CalculateItemTaxes(Item item);
    }
}