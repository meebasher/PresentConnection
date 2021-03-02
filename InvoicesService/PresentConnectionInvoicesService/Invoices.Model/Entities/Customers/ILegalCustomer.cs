namespace Invoices.Model.Entities.Customers
{
    public interface ILegalCustomer : IPerson
    {
        string LegalName { get; set; }
    }
}