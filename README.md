# lenovo-backlight-control
Just a simple wrapper for Windows 8/10 to control the keyboard backlight.
It relies on the Lenovo Companion App (or whatever it's called now) which has DLLs to control the backlight.

No extra care was taken with the code, no error checking, no smart functionality. Basically my first (and currently last) .NET console project.

Reverse engineering was done in July 2017 with JetBrains' DotPeek which resulted in some v01 code. Currently running fine on an X230.

The executable files are in the "releases" tab, including the AutoHotKey script which I use. You might need to edit that file. It turns on the backlight (dimmed) after a keypress or mouse move, turns it off after about 15 seconds.
The script can be adjusted with regards to timeout, light level, polling interval and Lenovo DLL path.


The easiest way to recreate the executable is to create a new .NET console project and paste the code inside the .cs file. The project file has been included but not tested.
