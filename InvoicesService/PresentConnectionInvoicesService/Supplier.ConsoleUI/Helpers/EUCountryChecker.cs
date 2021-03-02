using Supplier.Console.Services;
using System;
using System.Linq;

namespace Supplier.Console.Helpers
{
    public class EUCountryChecker : IEUCountryChecker
    {
        private IEUCountryRequester _euCountryRequester;
        public EUCountryChecker(IEUCountryRequester euCountryRequester)
        {
            _euCountryRequester = euCountryRequester ?? throw new ArgumentNullException(nameof(euCountryRequester));
        }

        /// <summary>
        /// Checks wether a coutry is in EU 
        /// </summary>
        /// <param name="countryName">Name of eu country</param>
        /// <returns>True if country is in EU</returns>
        public bool IsEUCoutry(string countryName)
        {
            var euCountries = _euCountryRequester.EUCountries;
            return euCountries.Any(c => c.CountryName.ToLower().Equals(countryName.ToLower()));
        }

        /// <summary>
        /// Gets 
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public string GetEUCoutryAlpha2Code(string countryName)
        {
            var euCountries = _euCountryRequester.EUCountries;
            try
            {
                return euCountries.First(c => c.CountryName.ToLower().Equals(countryName.ToLower())).Alpha2Code;
            }
            catch (Exception)
            {
                //log
                return string.Empty;
            }
        }
    }
}
