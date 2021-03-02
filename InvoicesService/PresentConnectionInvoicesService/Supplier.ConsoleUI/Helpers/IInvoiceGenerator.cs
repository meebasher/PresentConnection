using Invoices.Model.Entities;

namespace Supplier.Console.Helpers
{
    public interface IInvoiceGenerator
    {
        int GenerateInvoiceVAT(IPerson supplier, IPerson customer);
    }
}