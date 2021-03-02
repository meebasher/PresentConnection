using Invoices.Model.Entities.Locations;
using System.Collections.Generic;

namespace Supplier.Console.Services
{
    public interface IEUCountryRequester
    {
        IEnumerable<Country> EUCountries { get; }

        IEnumerable<Country> RefreshEUCountires();
    }
}