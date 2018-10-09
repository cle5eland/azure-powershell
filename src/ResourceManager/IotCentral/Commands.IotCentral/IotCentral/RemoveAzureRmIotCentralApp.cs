using Microsoft.Azure.Commands.IotCentral.Common;
using ResourceProperties = Microsoft.Azure.Commands.Management.IotCentral.Properties;
using System.Management.Automation;
using Microsoft.Azure.Management.IotCentral;

namespace Microsoft.Azure.Commands.Management.IotCentral
{
    [Cmdlet(VerbsCommon.Remove, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "IotCentralApp", SupportsShouldProcess = true)]
    [OutputType(typeof(bool))]
    public class RemoveAzureRmIotCentralApp : IotCentralFullParameterSetCmdlet
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
