using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.IotCentral.Models
{

    /// <summary>
    /// Defines values for 
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PSIotCentralAppSku
    {
        S1 = 1
    }
}
