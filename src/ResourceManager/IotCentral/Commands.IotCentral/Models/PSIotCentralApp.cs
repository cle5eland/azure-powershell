using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.IotCentral.Models
{
    public class PSIotCentralApp
    {
        public PSIotCentralApp(string name, string resourceId)
        {
            this.Name = name;
            this.Id = resourceId;
        }

        /// <summary>
        /// The Resource Id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        /// <summary>
        /// The Resource name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        /*
        /// <summary>
        /// The Resource type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; private set; }

        /// <summary>
        /// The Resource location.
        /// </summary>
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        /// <summary>
        /// The Resource tags.
        /// </summary>
        [JsonProperty(PropertyName = "tags")]
        public IDictionary<string, string> Tags { get; set; }

        /// <summary>
        /// Gets or sets a valid instance SKU.
        /// </summary>
        [JsonProperty(PropertyName = "sku")]
        public PSIotCentralAppSkuInfo Sku { get; set; }
        */
    }
}
