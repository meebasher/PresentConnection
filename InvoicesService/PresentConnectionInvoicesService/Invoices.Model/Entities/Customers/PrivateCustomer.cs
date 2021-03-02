namespace Invoices.Model.Entities.Customers
{
    public class PrivateCustomer : Person, IPrivateCustomer
    {
        public string PersonName { get; set; }
    }
}
