[CmdletBinding()]
PARAM(
	[Parameter(Mandatory=$true)]
	[ValidateSet("v4.0.30319","v2.0.50727")]
	[string]$SetVersion,
	[Parameter(Mandatory=$false)]
	[ValidateNotNullOrEmpty()]
	[string]$IPWizardConfig="C:\Program Files (x86)\Microsoft System Center 2012\Orchestrator\Integration Toolkit\Bin\IPWizard.exe.config"
)

[void](New-PSDrive -Name HKCR -Root HKEY_CLASSES_ROOT -PSProvider Registry)
Set-ItemProperty -Path "HKCR:\Wow6432Node\CLSID\{A2FE38AE-8F0D-4bad-AC0D-C1DC1EFD392A}\InprocServer32" -Name "RuntimeVersion" -Value $SetVersion
Set-ItemProperty -Path "HKCR:\Wow6432Node\CLSID\{A2FE38AE-8F0D-4bad-AC0D-C1DC1EFD392A}\InprocServer32\7.0.0.0" -Name "RuntimeVersion" -Value $SetVersion
"<configuration><startup><supportedRuntime version=""$SetVersion"" /></startup></configuration>" | Out-File $IPWizardConfig
