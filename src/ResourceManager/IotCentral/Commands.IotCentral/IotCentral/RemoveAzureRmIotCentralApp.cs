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
    [OutputType(typeof(PSIotCentralApp))]
    public class RemoveAzureRmIotCentralApp : IotCentralBaseCmdlet
    {
        const string InteractiveIotCentralParameterSet = "InteractiveIotCentralParameterSet";
        const string ResourceIdParameterSet = "ResourceIdParameterSet";
        const string InputObjectParameterSet = "InputObjectParameterSet";

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "Name of the Resource Group",
            ParameterSetName = InteractiveIotCentralParameterSet)]
        [ResourceGroupCompleter]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "Name of the Iot Central Application Resource",
            ParameterSetName = InteractiveIotCentralParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Iot Central Application Resource Id",
            ParameterSetName = ResourceIdParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            HelpMessage = "Iot Central Application Input Object",
            ParameterSetName = InputObjectParameterSet)]
        [ValidateNotNullOrEmpty]
        public PSIotCentralApp InputObject { get; set; }



        public override void ExecuteCmdlet()
        {
            if (ShouldProcess(Name, ResourceProperties.Resources.NewIotCentralApp))
            {
                switch (ParameterSetName)
                {
                    case InteractiveIotCentralParameterSet:
                        break;
                    case InputObjectParameterSet:
                        this.ResourceGroupName = this.InputObject.Resourcegroup;
                        this.Name = this.InputObject.Name;
                        break;
                    case ResourceIdParameterSet:
                        var identifier = new ResourceIdentifier(this.ResourceId);
                        this.ResourceGroupName = identifier.ResourceGroupName;
                        this.Name = identifier.ResourceName;
                        break;
                    default:
                        throw new PSArgumentException("BadParameterSetName");
                }
                this.IotCentralClient.Apps.Delete(this.ResourceGroupName, this.Name);
            }
        }
    }
}
