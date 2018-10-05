


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Management.Internal.Resources;
using Microsoft.Azure.Management.IotCentral;

namespace Microsoft.Azure.Commands.IotCentral.Common
{
    public class IotCentralBaseCmdlet : AzureRMCmdlet
    {
        private IIotCentralClient iotCentralClient;

        private IResourceManagementClient resourceManagementClient;

        const string InteractiveIotCentralParameterSet = "InteractiveIotCentralParameterSet";

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
