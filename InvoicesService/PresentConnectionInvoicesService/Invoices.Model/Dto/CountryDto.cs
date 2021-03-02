using Newtonsoft.Json;

namespace Invoices.Model.Dto
{
    public class CountryDto
    {
        [JsonProperty(PropertyName = "name")]
        public string CountryName { get; set; }
        [JsonProperty(PropertyName = "alpha2Code")]
        public string Alpha2Code { get; set; }
    }
}
