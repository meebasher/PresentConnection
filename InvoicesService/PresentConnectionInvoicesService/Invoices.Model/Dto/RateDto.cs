using Newtonsoft.Json;

namespace Invoices.Model.Dto
{
    public class RateDto
    {
        [JsonProperty(PropertyName = "standard_rate")]
        public int Rate { get; set; }
    }
}
