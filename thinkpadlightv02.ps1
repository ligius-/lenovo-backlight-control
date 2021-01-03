# Run as %SystemRoot%\SysWOW64\WindowsPowerShell\v1.0\powershell.exe thinkpadlightv02.ps1 'C:\Users\All Users\Lenovo\ImController\Plugins\ThinkKeyboardPlugin\x86\Keyboard_Core.dll' 0
# Or you can generate an EXE using Win-PS2EXE - you need to specify the platform to be x86!

Add-Type -path $args[0]
(New-Object Keyboard_Core.KeyboardControl).SetKeyboardBackLightStatus($args[1], $null)
