using System.Management.Automation;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.Internal.Resources;
using Microsoft.Azure.Management.IotCentral;

namespace Microsoft.Azure.Commands.IotCentral.Common
{
    public abstract class IotCentralBaseCmdlet : AzureRMCmdlet
    {
        private IIotCentralClient iotCentralClient;

        private IResourceManagementClient resourceManagementClient;

        protected const string InteractiveIotCentralParameterSet = "InteractiveIotCentralParameterSet";
        protected const string ResourceIdParameterSet = "ResourceIdParameterSet";
        protected const string InputObjectParameterSet = "InputObjectParameterSet";

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


        protected IIotCentralClient IotCentralClient
        {
            get
            {
                if (this.iotCentralClient == null)
                {
                    this.iotCentralClient = AzureSession.Instance.ClientFactory.CreateArmClient<IotCentralClient>(DefaultProfile.DefaultContext, AzureEnvironment.Endpoint.ResourceManager);
                }
                return this.iotCentralClient;
            }
        }

        protected IResourceManagementClient ResourceManagementClient
        {
            get
            {
                if (this.resourceManagementClient == null)
                {
                    this.resourceManagementClient = AzureSession.Instance.ClientFactory.CreateArmClient<ResourceManagementClient>(DefaultProfile.DefaultContext, AzureEnvironment.Endpoint.ResourceManager);
                }
                return this.resourceManagementClient;
            }
        }


    }
}
