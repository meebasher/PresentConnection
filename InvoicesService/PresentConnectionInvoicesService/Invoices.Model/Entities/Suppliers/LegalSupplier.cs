namespace Invoices.Model.Entities.Suppliers
{
    public class LegalSupplier : Person, ILegalSupplier
    {
        public string LegalName { get; set; }
    }
}
