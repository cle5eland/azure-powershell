using Microsoft.Azure.Commands.IotCentral.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.IotCentral.Common
{
    public abstract class FullParameterSetCmdlet : IotCentralBaseCmdlet
    {
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

        protected void SetNameAndResourceGroup()
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
        }
    }
}
