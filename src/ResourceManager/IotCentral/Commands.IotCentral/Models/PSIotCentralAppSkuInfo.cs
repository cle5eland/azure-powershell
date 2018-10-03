using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
