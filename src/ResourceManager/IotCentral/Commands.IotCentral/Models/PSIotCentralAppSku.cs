using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
