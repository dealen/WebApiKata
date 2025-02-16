param(
    [string]$ResourceGroupName = "jm-rg-test",
    [string]$Location = "westeurope"
)

# Check if the resource group already exists.  Important for idempotency.
if (!(Get-AzResourceGroup -Name $ResourceGroupName -ErrorAction SilentlyContinue)) {
    New-AzResourceGroup -Name $ResourceGroupName -Location $Location
    Write-Host "Resource group '$ResourceGroupName' created in '$Location'."
}
else {
    Write-Host "Resource group '$ResourceGroupName' already exists."
}

# Example:  Deploy a Bicep template (optional - add if you have a Bicep file).
# New-AzResourceGroupDeployment -ResourceGroupName $ResourceGroupName -TemplateFile "main.bicep"