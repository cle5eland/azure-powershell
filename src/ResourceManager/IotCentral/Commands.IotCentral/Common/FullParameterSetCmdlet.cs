using Microsoft.Azure.Commands.IotCentral.Models;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.IotCentral.Common
{
    public abstract class IotCentralFullParameterSetCmdlet : IotCentralBaseCmdlet
    {
        /// <summary>
        /// Iot Central Application ResourceId
        /// </summary>
        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Iot Central Application Resource Id",
            ParameterSetName = ResourceIdParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        /// <summary>
        /// Iot Central Application Input Object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            HelpMessage = "Iot Central Application Input Object",
            ParameterSetName = InputObjectParameterSet)]
        [ValidateNotNullOrEmpty]
        public PSIotCentralApp InputObject { get; set; }

        /// <summary>
        /// Uses the applicable parameter group to set the Name and ResouceName for the current execution.
        /// </summary>
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
