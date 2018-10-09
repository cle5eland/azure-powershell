using Microsoft.Azure.Commands.IotCentral.Common;
using Microsoft.Azure.Commands.IotCentral.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Management.IotCentral;
using Microsoft.Azure.Management.IotCentral.Models;
using System.Collections;
using System.Management.Automation;
using ResourceProperties = Microsoft.Azure.Commands.Management.IotCentral.Properties;

namespace Microsoft.Azure.Commands.Management.IotCentral
{
    [Cmdlet(VerbsCommon.Set, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "IotCentralApp", SupportsShouldProcess = true)]
    [OutputType(typeof(PSIotCentralApp))]
    public class SetAzureRmIotCentralApp : IotCentralFullParameterSetCmdlet
    {
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

        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        public override void ExecuteCmdlet()
        {
            if (ShouldProcess(Name, ResourceProperties.Resources.SetIotCentralApp))
            {
                this.SetNameAndResourceGroup();
                AppPatch applicationPatch = CreateApplicationPatch();
                App updatedIotCentralApplication = this.IotCentralClient.Apps.Update(this.ResourceGroupName, this.Name, applicationPatch);
                this.WriteObject(IotCentralUtils.ToPSIotCentralApp(updatedIotCentralApplication));
            }
        }

        private AppPatch CreateApplicationPatch()
        {
            App existingIotCentralApplication = this.GetApplication();
            this.SetApplicationDisplayName(existingIotCentralApplication);
            this.SetApplicationTags(existingIotCentralApplication);
            AppPatch iotCentralAppPatch = IotCentralUtils.CreateAppPatch(existingIotCentralApplication);
            return iotCentralAppPatch;
        }

        private void SetApplicationDisplayName(App application)
        {
            application.DisplayName = this.DisplayName ?? application.DisplayName;
        }

        private void SetApplicationTags(App application)
        {
            if (this.Tag != null)
            {
                application.Tags = TagsConversionHelper.CreateTagDictionary(this.Tag, true);
            }
        }

        private App GetApplication()
        {
            App existingIotCentralApplication = this.IotCentralClient.Apps.Get(this.ResourceGroupName, this.Name);
            if (existingIotCentralApplication == null)
            {
                throw new PSArgumentException("Requested Iot Central Application does not exist");
            }
            return existingIotCentralApplication;
        }
    }
}
