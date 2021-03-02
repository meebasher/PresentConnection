using Invoices.Model.Entities;
using Invoices.Model.Entities.Customers;

namespace Supplier.Console.Factories
{
    public class CustomerFactory
    {
        public static ILegalCustomer GetLegalCustomer(string organizationName, string countryName, bool isVatPayer)
        {
            return new LegalCustomer
            {
                Country = countryName,
                LegalName = organizationName,
                IsVatPayer = isVatPayer
            };
        }

        public static IPrivateCustomer GetPrivateCustomer(string customerName, string countryName, bool isVatPayer)
        {
            return new PrivateCustomer
            {
                Country = countryName,
                PersonName = customerName,
                IsVatPayer = isVatPayer
            };
        }
    }
}