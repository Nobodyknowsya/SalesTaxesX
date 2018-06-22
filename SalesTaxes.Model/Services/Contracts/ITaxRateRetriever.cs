namespace SalesTaxes.Model.Services.Contracts
{
    public interface ITaxRateRetriever
    {
        decimal GetTaxRate();
        decimal GetImportTaxRate();
    }
}