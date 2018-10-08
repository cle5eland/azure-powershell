using Microsoft.Azure.Commands.IotCentral.Common;
using Microsoft.Azure.Commands.IotCentral.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using ResourceProperties = Microsoft.Azure.Commands.Management.IotCentral.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.IotCentral;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;

namespace Microsoft.Azure.Commands.Management.IotCentral
{
    [Cmdlet("Remove", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "IotCentralApp", SupportsShouldProcess = true)]
    [OutputType(typeof(bool))]
    public class RemoveAzureRmIotCentralApp : FullParameterSetCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        public override void ExecuteCmdlet()
        {
            if (ShouldProcess(Name, ResourceProperties.Resources.RemoveIotCentralApp))
            {
                this.SetNameAndResourceGroup();
                this.IotCentralClient.Apps.Delete(this.ResourceGroupName, this.Name);
                if (PassThru)
                {
                    this.WriteObject(true);
                }
            }
        }
    }
}
