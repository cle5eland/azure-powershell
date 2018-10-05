using Commands.IotCentral.Common;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
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
        private ResourceIdentifier identifier;

        private ResourceIdentifier Identifier
        {
            get
            {
                if (this.identifier == null)
                {
                    this.identifier = new ResourceIdentifier(this.Id);
                }
                return this.identifier;
            }
            set
            {
                this.identifier = value;
            }
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

        /// <summary>
        /// Gets the ID of the application.
        /// </summary>
        [JsonProperty(PropertyName = "properties.applicationId")]
        public string ApplicationId { get; private set; }

        /// <summary>
        /// Gets or sets the display name of the application.
        /// </summary>
        [JsonProperty(PropertyName = "properties.displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the subdomain of the application.
        /// </summary>
        [JsonProperty(PropertyName = "properties.subdomain")]
        public string Subdomain { get; set; }

        /// <summary>
        /// Gets or sets the ID of the application template, which is a
        /// blueprint that defines the characteristics and behaviors of an
        /// application. Optional; if not specified, defaults to a blank
        /// blueprint and allows the application to be defined from scratch.
        /// </summary>
        [JsonProperty(PropertyName = "properties.template")]
        public string Template { get; set; }

        /// <summary>
        /// The subscription identifier.
        /// </summary>
        [JsonProperty(PropertyName = "subscriptionid")]
        public string Subscriptionid {
            get
            {
                return this.Identifier.Subscription;
            }
        }

        /// <summary>
        /// The resource group name
        /// </summary>
        [JsonProperty(PropertyName = "resourcegroup")]
        public string Resourcegroup
        {
            get
            {
                return this.Identifier.ResourceGroupName;
            }
        }

    }
}
