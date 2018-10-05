using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.IotCentral.Models
{
    public class PSIotCentralAppProperties
    {
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
    }
}
