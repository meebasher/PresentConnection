namespace Invoices.Model.Entities.Customers
{
    public interface IPrivateCustomer : IPerson
    {
        string PersonName { get; set; }
    }
}