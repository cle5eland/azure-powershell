# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------


#################################
## IotCentral Cmdlets			   ##
#################################

$global:resourceType = "Microsoft.IoTCentral/IotApps"

<#
.SYNOPSIS
Tests creating Resource Group and Iot Central App
#>

function Test-CreateSimpleIotCentralApp{
	# Setup
	$rgname = Get-ResourceGroupName
	$rname = Get-ResourceName
	$location = Get-Location "Microsoft.IoTCentral" "IotApps" 
	
	try
	{
		# Test

		# Create Resource Group
		New-AzureRmResourceGroup -Name $rgname -Location $rglocation

		# Create App
		New-AzureRmIotCentralApp -ResourceGroupName $rgname -Name $rname -Subdomain $rname
		$actual = Get-AzureIotCentralApp -ResourceGroupName $rgname -Name $rname

		$list = Get-AzureRmIotCentralApp -ResourceGroupName $rgname
	
		# Assert

		# Name
		Assert-AreEqual $actual.Name $rname
		Assert-AreEqual $expected.ResourceGroupName $actual.ResourceGroupName
		Assert-AreEqual $actual.Subdomain $rname
		Assert-AreEqual $actual.ResourceType $resourceType
		Assert-AreEqual 1 @($list).Count
		Assert-AreEqual $actual.Name $list[0].Name
	}
	finally{
		# Clean up
		Clean_ResourceGroup $rgname
	}
}

function Test-CreateComplexIotCentralApp{
	# Setup
	$rgname = Get-ResourceGroupName
	$rname = Get-ResourceName
	$subdomain = ($rname) + "subdomain"
	$location = Get-Location "Microsoft.IoTCentral" "IotApps"
	$sku = "S1"
	$displayName = "Custom IoT Central App DisplayName"
	$tagKey = "key1"
	$tagValue = "value1"
	$tags = @{ $tagKey = $tagValue }
	
	try
	{
		# Test

		# Create Resource Group
		New-AzureRmResourceGroup -Name $rgname -Location $rglocation

		# Create App
		New-AzureRmIotCentralApp -ResourceGroupName $rgname -Name $rname -Subdomain $subdomain -Sku $sku -DisplayName $displayName -Tag $tags
		$actual = Get-AzureIotCentralApp -ResourceGroupName $rgname -Name $rname

		$list = Get-AzureRmIotCentralApp -ResourceGroupName $rgname
	
		# Assert

		# Name
		Assert-AreEqual $actual.Name $rname
		Assert-AreEqual $expected.ResourceGroupName $actual.ResourceGroupName
		Assert-AreEqual $actual.Subdomain $subdomain
		Assert-AreEqual $actual.Sku $sku
		Assert-AreEqual $actual.DisplayName $displayName
		Assert-AreEqual $actual.Tag.Item($tagkey) $tagvalue
		Assert-AreEqual $actual.ResourceType $resourceType
		Assert-AreEqual 1 @($list).Count
		Assert-AreEqual $actual.Name $list[0].Name
	}
	finally{
		# Clean up
		Clean_ResourceGroup $rgname
	}
}

<#
.SYNOPSIS
Test Iot Central Application Lifecycle PowerShell cmdlets
#>
function Test-AzureRmIotCentralAppLifecycle
{
	$Location = Get-Location "Microsoft.IoTCentral" "IotApps" 
	$IotCentralAppName = getAssetName
	$IotCentralSubdomain = getAssetName
	$ResourceGroupName = getAssetName 
	$Sku = "S1"
	$DisplayName = "Custom IoT Central App DisplayName"
	$Tag1Key = "key1"
	$Tag2Key = "key2"
	$Tag1Value = "value1"
	$Tag2Value = "value2"

	# Get all Iot Central Apps in the subscription
	$allIotCentralApps = Get-AzureRmIotCentralApp

	Assert-True { $allIotCentralApps[0].Type -eq $global:resourceType }
	Assert-True { $allIotCentralApps.Count -gt 1 }

	# Create or Update Resource Group
	$resourceGroup = New-AzureRmResourceGroup -Name $ResourceGroupName -Location $Location 

	# Create Iot Central App
	$tags = @{ $Tag1Key = $Tag1Value }
	$newAzureIotCentralApp = New-AzureRmIotCentralApp -Name $IotCentralAppName -ResourceGroupName $ResourceGroupName -Subdomain $IotCentralSubdomain -Location $Location -Sku $Sku -Tag $tags -DisplayName $DisplayName

	# Assert that there is only 1 Iot Central App in the RG
	AssertNumberOfAppsInRG -ResourceGroupName $ResourceGroupName -count 1
	
	# Get Iot Central App
	$iotCentralApp = Get-AzureRmIotCentralApp -ResourceGroupName $ResourceGroupName -Name $IotCentralAppName 

	Assert-True { $iotCentralApp.Name -eq $IotCentralAppName }
	Assert-True { $iotCentralApp.Sku -eq $Sku }
	Assert-True { $iotCentralApp.Subdomain -eq $IotCentralSubdomain }
	Assert-True { $iotCentralApp.DisplayName -eq $DisplayName }
	Assert-True { $iotCentralApp.Location -eq $Location }
	Assert-True { $iotCentralApp.Tag.Item($Tag1Key) -eq $Tag1Value}

	# Update Display Name of Iot Central App, Test Piping
	$NewDisplayName = "Updated Display Name"
	$updatedIotCentralApp = Set-AzureRmIotCentralApp -Name $IotCentralAppName -ResourceGroupName $ResourceGroupName -DisplayName $NewDisplayName

	# Confirm Display Name is updated
	Assert-True { $updatedIotCentralApp.DisplayName -eq $DisplayName }

	# Get the app, confirm change is propogated
	$updatedIotCentralApp = Get-AzureRmIotCentralApp -Name $IotCentralAppName -ResourceGroupName $ResourceGroupName
	Assert-True { $updatedIotCentralApp.DisplayName -eq $DisplayName }

	# Change Tags
	$tags = @{ $Tag2Key = $Tag2Value }
	$updatedIotCentralApp = Set-AzureRmIotCentralApp -Name $IotCentralAppName -ResourceGroupName $ResourceGroupName -Tag $tags
	Assert-True { $updatedIotCentralApp.Tag.Count -eq 1 }
	Assert-True { $updatedIotCentralApp.Tag.Item($Tag2Key) -eq $Tag2Value }

	# Remove IotCentral App
	Remove-AzureRmIotCentralApp -ResourceGroupName $ResourceGroupName -Name $IotCentralAppName

	AssertNumberOfAppsInRG -ResourceGroupName $ResourceGroupName -count 0

	# Remove Resource Group
	Remove-AzureRmResourceGroup $ResourceGroupName
}

function GetNumberElementsInResourceGroup{
	Param($ResourceGroupName)
	$iotCentralAppsInRG = Get-AzureRmIotCentralApp -ResourceGroupName $ResourceGroupName
	return $iotCentralAppsInRG.Count
}

function AssertNumberOfAppsInRG{
	Param($ResourceGroupName,$count)
	$countInRG = GetNumberElementsInResourceGroup -ResourceGroupName $ResourceGroupName
	Assert-True($countInRG -eq $count)
}
