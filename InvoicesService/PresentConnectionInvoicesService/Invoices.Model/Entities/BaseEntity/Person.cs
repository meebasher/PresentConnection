using System;

namespace Invoices.Model.Entities
{
    public abstract class Person : IPerson
    {
        public string Country { get; set; }
        public bool IsVatPayer { get; set; }
    }
}
