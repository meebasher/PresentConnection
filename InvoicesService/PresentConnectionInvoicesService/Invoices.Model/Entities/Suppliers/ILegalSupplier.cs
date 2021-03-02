namespace Invoices.Model.Entities.Suppliers
{
    public interface ILegalSupplier : IPerson
    {
        string LegalName { get; set; }
    }
}