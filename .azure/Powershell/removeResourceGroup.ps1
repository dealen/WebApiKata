param(
    [string]$ResourceGroupName = "jm-rg-test"
)

# Dry run (WhatIf) - Highly recommended!
Write-Host "Performing a dry run (WhatIf) to see what would be deleted..."
Remove-AzResourceGroup -Name $ResourceGroupName -WhatIf

# Prompt for confirmation (unless -Force is used).
Write-Host "This will PERMANENTLY delete the resource group '$ResourceGroupName' and ALL its contents."
Write-Host "Type 'YES' (case-sensitive) to confirm, or anything else to cancel."

$confirmation = Read-Host

if ($confirmation -eq "YES") {
    Remove-AzResourceGroup -Name $ResourceGroupName -Force
    Write-Host "Resource group '$ResourceGroupName' deleted."
}
else {
    Write-Host "Deletion cancelled."
}