using Invoices.Model.Dto;
using Invoices.Model.Entities.Locations;
using Supplier.REST;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Supplier.Console.Services
{
    public class EUCountryRequester : IEUCountryRequester
    {
        private readonly IRequsetRestAgent _requestAgent;

        public IEnumerable<Country> EUCountries { get; private set; }

        public EUCountryRequester(IRequsetRestAgent requestAgent)
        {
            _requestAgent = requestAgent ?? throw new ArgumentNullException(nameof(requestAgent));

            EUCountries = FetchEUCountries();
        }

        private IEnumerable<Country> FetchEUCountries()
        {
            var euCoutriesResponse = _requestAgent.SendGETRequest("https://restcountries.eu/rest/v2/regionalbloc/eu");

            var euCountries = _requestAgent.GetJsonDeserializedContent<IEnumerable<CountryDto>>(euCoutriesResponse).ToList();

            var countriesToReturn = euCountries.Select(x => new Country { Alpha2Code = x.Alpha2Code, CountryName = x.CountryName });

            return countriesToReturn;
        }

        public IEnumerable<Country> RefreshEUCountires()
        {
            EUCountries = FetchEUCountries();
            return EUCountries;
        }
    }
}
