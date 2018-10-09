using Newtonsoft.Json;

namespace Microsoft.Azure.Commands.IotCentral.Models
{
    public class PSIotCentralAppSkuInfo
    {
        /// <summary>
        /// Gets or sets the name of the SKU. Possible values include: 'S1'
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public PSIotCentralAppSku Name { get; set; }
    }
}
