namespace Invoices.Model.Entities.Customers
{
    public class LegalCustomer : Person, ILegalCustomer
    {
        public string LegalName { get; set; }
    }
}
