Remove-Item -Path .\AutomationWebApi\bin\Debug\netcoreapp3.1\Plugins -Recurse
$plugins = New-Item -Path .\AutomationWebApi\bin\**\netcoreapp3.1\ -Name Plugins -ItemType "directory"
Get-ChildItem -Path ./ -Filter *Plugin | ForEach-Object {
$destination = New-Item -Path $plugins -Name $_.Name -ItemType "directory"
Get-ChildItem -Path $_ -Include *Plugin.dll, *Plugin.deps.json -Recurse | Copy-Item -Destination $destination
}

 