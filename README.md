# lenovo-backlight-control
Just a simple wrapper for Windows 8/10 to control the keyboard backlight on certain Lenovo Thinkpad laptops. It has been confirmed to work on X220, X230, T490s, X1 (4th) and possibly other models.

It relies on the **Lenovo Vantage** App (previously called **Lenovo Companion**) which has DLLs to control the backlight.

Usage
--
The executable files are in the "releases" tab, including the AutoHotKey script which I use. You might need to edit that file. It turns on the backlight (dimmed) after a keypress or mouse move, turns it off after about 15 seconds.
The AHK script can be adjusted with regards to timeout, light level, polling interval and Lenovo DLL path.

No warranty, use at your own risk. Avoid using random executables from the Internet.

Technical details
--

No extra care was taken with the code, no error checking, no smart functionality. Basically my first (and currently last) .NET console project.

Reverse engineering was done in July 2017 with JetBrains' DotPeek which resulted in the v01 code. Currently running fine on an X230.

It directly calls the function _SetKeyboardBackLightStatus_ in _x86\Keyboard_Core.dll_ which sets a keyboard backlight level from 0 to 2. A similar approach can be used to control other functionality, for example setting the charging threshold level.

Since the DLL is compiled with **x86**, the controlling script must also run in x86 mode, even on a x64 machine. In this case, the software has been compiled with an x86 target.

There is also a demo PowerShell script which achieves the same, however it takes about 1 second to start up:

```powershell
# Run as %SystemRoot%\SysWOW64\WindowsPowerShell\v1.0\powershell.exe thinkpadlightv02.ps1 'C:\Users\All Users\Lenovo\ImController\Plugins\ThinkKeyboardPlugin\x86\Keyboard_Core.dll' 0
# Or you can generate an EXE using Win-PS2EXE - you need to specify the platform to be x86!

Add-Type -path $args[0]
(New-Object Keyboard_Core.KeyboardControl).SetKeyboardBackLightStatus($args[1], $null)
```

For newer laptops, a similar method is working, however it applies to a different x64 DLL. If there is enough interest, I can develop a similar program, however newer laptops generally have an automatic backlight. An example that has been tested on Yoga C940:
```powershell
$assembly = [System.Reflection.Assembly]::LoadFrom("c:\Users\All Users\Lenovo\ImController\Plugins\IdeaNotebookPlugin\x64\IdeaNotebookPlugin.dll" )
$agent = $assembly.GetTypes()[0].GetMethod("GetInstance").Invoke($null,$null)

# or

Add-Type -Path "c:\Users\All Users\Lenovo\ImController\Plugins\IdeaNotebookPlugin\x64\IdeaNotebookPlugin.dll"
Add-Type -Path "c:\Users\All Users\Lenovo\ImController\Plugins\IdeaNotebookPlugin\x64\Contract_Keyboard.dll"
$BindingFlags = 'static','nonpublic','instance'
[Lenovo.Modern.Plugins.IdeaNotebookPlugin.NativeHelper]::DoDeviceIoControl[0]

```
