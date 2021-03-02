using Invoices.Model.Dto;
using Supplier.REST;
using System;

namespace Supplier.Console.Services
{
    public class VATRequester : IVATRequester
    {
        private readonly IRequsetRestAgent _requestAgent;
        public VATRequester(IRequsetRestAgent requestAgent)
        {
            _requestAgent = requestAgent ?? throw new ArgumentNullException(nameof(requestAgent));
        }

        /// <summary>
        /// Gets VAT rate from specific country in EU region
        /// </summary>
        /// <param name="countyCode">ISO 3166-1 alpha-2 country code</param>
        /// <returns>Coutry VAT rate</returns>
        /// <returns>If country is not in EU returns 0</returns>
        public int GetEUCountryVAT(string countyCode)
        {
            if (countyCode.Length != 2)
            {
                throw new ArgumentException(nameof(countyCode));
            }

            var vatResponse = _requestAgent.SendGETRequest($"https://api.vatzen.com/v1/rate/{countyCode}/?api_key=c772fea68337650344aa84543476d94abc04b6ff");

            if (vatResponse == null)
            {
                throw new ArgumentException(nameof(countyCode));
            }

            var vatRate = _requestAgent.GetJsonDeserializedContent<RateDto>(vatResponse).Rate;

            return vatRate;

        }

    }
}
