using Microsoft.Azure.Commands.IotCentral.Common;
using Microsoft.Azure.Commands.IotCentral.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Azure.Management.IotCentral;
using Microsoft.Azure.Management.IotCentral.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Management.IotCentral
{
    [Cmdlet("Set", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "IotCentralApp", SupportsShouldProcess = true)]
    [OutputType(typeof(PSIotCentralApp))]
    public class SetAzureRmIotCentralApp : IotCentralBaseCmdlet
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

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Custom Display Name of the Iot Central Application")]
        [ValidateNotNullOrEmpty]
        public string DisplayName { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Iot Central Application Resource Tags")]
        [ValidateNotNullOrEmpty]
        public Hashtable Tag { get; set; }

        public override void ExecuteCmdlet()
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
            App existingIotCentralApplication = this.IotCentralClient.Apps.Get(this.ResourceGroupName, this.Name);
            if (existingIotCentralApplication == null)
            {
                throw new PSArgumentException("Requested Iot Central Application does not exist");
            }
            existingIotCentralApplication.DisplayName = this.DisplayName ?? existingIotCentralApplication.DisplayName;
            if (this.Tag != null)
            {
                existingIotCentralApplication.Tags = TagsConversionHelper.CreateTagDictionary(this.Tag, true);
            }

            var updatedIotCentralApplication = this.IotCentralClient.Apps.CreateOrUpdate(this.ResourceGroupName, this.Name, existingIotCentralApplication);
            this.WriteObject(updatedIotCentralApplication);
        }
    }
}
