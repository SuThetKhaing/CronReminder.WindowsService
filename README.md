
// Sample Windows Service Link
https://www.c-sharpcorner.com/article/create-windows-services-in-c-sharp/

To install Windows Service 
==>> Search "Command Prompt" and run as administrator.

==>> Fire the below command in the command prompt and press ENTER.
	cd C:\Windows\Microsoft.NET\Framework\v4.0.30319 
	
==>> Now Go to your project source folder > bin > Debug and copy the full path of your Windows Service exe file.
	Syntax:
	InstallUtil.exe + Your copied path + \your service name + .exe
	My path:
	InstallUtil.exe C:\Development\SuThet\Cron Reminder\20240614\Debug\CronReminder.WindowsService.exe
	
==>> Check the status of a Windows Service.
	Open services by following the below steps:
	Press the Window key + R.
	Type services.msc
	Find your Service.

==>> Uninstalling a Windows Service
	If you want to uninstall your service, fire the below command.
	Syntax InstallUtil.exe -u + Your copied path + \your service name + .exe
	My path:
	InstallUtil.exe -u C:\Development\SuThet\Cron Reminder\20240614\Debug\CronReminder.WindowsService.exe