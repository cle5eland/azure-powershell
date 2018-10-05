﻿using Microsoft.Azure.Commands.IotCentral.Common;
using Microsoft.Azure.Commands.IotCentral.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.Internal.Resources;
using Microsoft.Azure.Management.IotCentral;
using Microsoft.Azure.Management.IotCentral.Models;
using ResourceProperties = Microsoft.Azure.Commands.Management.IotCentral.Properties;
using System.Management.Automation;
using Commands.IotCentral.Common;
using System.Collections;

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

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Iot Central Application Template Name")]
        [ValidateNotNullOrEmpty]
        public string Template { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Iot Central Application Scale Unit")]
        [ValidateNotNullOrEmpty]
        public PSIotCentralAppSku? Sku { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Location of the Iot Central Application resource. Default is the location of the resource group.")]
        [LocationCompleter("Microsoft.IoTCentral/IotApps")]
        [ValidateNotNullOrEmpty]
        public string Location { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Iot Central Application Resource Tags")]
        [ValidateNotNullOrEmpty]
        public Hashtable Tag { get; set; }



        public override void ExecuteCmdlet()
        {
            if (ShouldProcess(Name, ResourceProperties.Resources.NewIotCentralApp))
            {
                var availabilityInfo = this.IotCentralClient.Apps.CheckNameAvailability(this.Name);
                if (!(availabilityInfo.NameAvailable is true))
                {
                    throw new PSArgumentException(availabilityInfo.Message);
                }
                var iotCentralApp = new App()
                {
                    DisplayName = this.GetDisplayName(),
                    Subdomain = this.Subdomain,
                    Template = this.Template,
                    Sku = new AppSkuInfo() { Name = this.GetAppSkuName() },
                    Location = this.GetLocation()
                };

                this.IotCentralClient.Apps.CreateOrUpdate(this.ResourceGroupName, this.Name, iotCentralApp);
                App createdIotCentralApp = this.IotCentralClient.Apps.Get(this.ResourceGroupName, this.Name);
                this.WriteObject(IotCentralUtils.ToPSIotCentralApp(createdIotCentralApp), false);
            }
            
        }

        private string GetAppSkuName()
        {
            return this.Sku?.ToString() ?? PSIotCentralAppSku.S1.ToString();
        }

        private string GetDisplayName()
        {
            if (string.IsNullOrEmpty(this.DisplayName))
            {
                return this.Name;
            }
            return this.DisplayName;
        }

        private string GetLocation()
        {
            if (string.IsNullOrEmpty(this.Location))
            {
                return this.ResourceManagementClient.ResourceGroups.Get(ResourceGroupName).Location;
            }
            return this.Location;
        }
    }
}
