using Invoices.Model.Entities.Suppliers;

namespace Supplier.Console.Factories
{
    public static class SupplierFactory
    {
        public static ILegalSupplier GetLegalSupplier(string legalName, string countryName, bool isVatPayer)
        {
            return new LegalSupplier
            {
                LegalName = legalName,
                Country = countryName,
                IsVatPayer = isVatPayer
            };
        }
    }
}
