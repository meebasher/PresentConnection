namespace Invoices.Model.Entities
{
    public interface IPerson
    {
        string Country { get; set; }
        bool IsVatPayer { get; set; }
    }
}