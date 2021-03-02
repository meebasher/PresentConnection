using Supplier.Console.Factories;
using Supplier.Console.Helpers;
using Supplier.Console.Services;
using Supplier.REST;

namespace Supplier.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine();

            var supplier = SupplierFactory.GetLegalSupplier("Mario LTD", "Lithuania", true);
            var customer = CustomerFactory.GetLegalCustomer("Lorum LTD", "Poland", false);
            var requestAgent = new RequsetRestAgent();
            var vATRequester = new VATRequester(requestAgent);
            var countryRequester = new EUCountryRequester(requestAgent);
            var euCountryChecker = new EUCountryChecker(countryRequester);
            var invoiceGenerator = new InvoiceGenerator(vATRequester, euCountryChecker);

            var vATPercentage = invoiceGenerator.GenerateInvoiceVAT(supplier, customer);

            System.Console.WriteLine($"Invoice VAT percentage for '{customer.LegalName}' = {vATPercentage}%");
            System.Console.ReadKey();
        }
    }
}
