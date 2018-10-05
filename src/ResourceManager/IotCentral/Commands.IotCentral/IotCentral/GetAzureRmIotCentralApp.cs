using Microsoft.Azure.Commands.IotCentral.Common;
using Microsoft.Azure.Commands.IotCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using ResourceManager = Microsoft.Azure.Commands.ResourceManager;
using Microsoft.Azure.Management.IotCentral.Models;
using Microsoft.Azure.Management.IotCentral;
using Commands.IotCentral.Common;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;

namespace Commands.IotCentral.IotCentral
{
    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "IotCentralApp", DefaultParameterSetName = ListIotCentralAppsParameterSet)]
    [OutputType(typeof(PSIotCentralApp))]
    public class GetAzureRmIotCentralApp : IotCentralBaseCmdlet
    {
        const string InteractiveIotCentralParameterSet = "InteractiveIotCentralParameterSet";
        const string ListIotCentralAppsParameterSet = "ListIotCentralAppsParameterSet";
        const string ResourceIdParameterSet = "ResourceIdParameterSet";

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "Name of the Resource Group",
            ParameterSetName = InteractiveIotCentralParameterSet)]
        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "Name of the Resource Group",
            ParameterSetName = ListIotCentralAppsParameterSet)]
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
            Position = 1,
            HelpMessage = "Iot Central Application Resource Id",
            ParameterSetName = ResourceIdParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case InteractiveIotCentralParameterSet:
                    App iotCentralApp = this.IotCentralClient.Apps.Get(this.ResourceGroupName, this.Name);
                    this.WriteObject(IotCentralUtils.ToPSIotCentralApp(iotCentralApp));
                    break;
                case ListIotCentralAppsParameterSet:
                    if (string.IsNullOrEmpty(this.ResourceGroupName)){
                        IEnumerable<App> iotCentralAppsBySubscription = this.IotCentralClient.Apps.ListBySubscription();
                        this.WriteObject(IotCentralUtils.ToPSIotCentralApps(iotCentralAppsBySubscription));
                        break;
                    }
                    else
                    {
                        IEnumerable<App> iotCentralAppsByResourceGroup = this.IotCentralClient.Apps.ListByResourceGroup(this.ResourceGroupName);
                        this.WriteObject(IotCentralUtils.ToPSIotCentralApps(iotCentralAppsByResourceGroup));
                        break;
                    }
                case ResourceIdParameterSet:
                    ResourceIdentifier identifier = new ResourceIdentifier(this.ResourceId);
                    var app = this.IotCentralClient.Apps.Get(identifier.ResourceGroupName, identifier.ResourceName);
                    this.WriteObject(IotCentralUtils.ToPSIotCentralApp(app));
                    break;
                default:
                    throw new PSArgumentException("BadParameterSetName");
            }
        }

    }
}
