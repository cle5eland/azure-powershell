using Microsoft.Azure.Commands.IotCentral.Common;
using Microsoft.Azure.Commands.IotCentral.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
// using ResourceProperties = Microsoft.Azure.Commands.Management.IotCentral.Properties
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Management.IotCentral
{
    [Cmdlet("New", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "IotCentralApp", SupportsShouldProcess = true)]
    [OutputType(typeof(PSIotCentralApp))]
    public class NewAzureRmIotCentralApp : IotCentralBaseCmdlet
    {
        const string InteractiveIotCentralParameterSet = "InteractiveIotCentralParameterSet";
        
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
            Position = 2,
            HelpMessage = "Iot Central Application Subdomain",
            ParameterSetName = InteractiveIotCentralParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Subdomain { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Custom Display Name of the Iot Central Application")]
        [ValidateNotNullOrEmpty]

        public string DisplayName { get; set; }
        
        public override void ExecuteCmdlet()
        {
            // if(ShouldProcess(Name, ResourceProperties))
            // var testdata = new PSIotCentralApp("hello","world");
            this.WriteObject("Hello World");
        }
    }
}
