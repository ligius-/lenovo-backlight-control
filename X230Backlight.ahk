; Ligius (2017) - try to make the Thinkpad keyboard backlight behave more like a normal one
global START_WITH_BACKLIGHT := 1 ; set to 1 if you want backlight on startup, 0 otherwise
global BACKLIGHT_LEVEL := 1 ; set to 0 for no backlight, 1 for the first (dim level), 2 for the brightest level
global IDLE_DURATION := 15000 ; after how many milliseconds of inactivity the light should turn off
;global POLLING_PERIOD := 250 ; how often (ms) should the program check for inactivity; lower turns backlight on faster but drains cpu

; TODO: make the backlight wake up after screen off

;#InstallKeybdHook
;#InstallMouseHook
;SetTimer, Check, POLLING_PERIOD
;return

global wasOn := 0
setBacklight(START_WITH_BACKLIGHT ? BACKLIGHT_LEVEL : 0)

; https://autohotkey.com/board/topic/94002-send-escape-key-after-idle-time/
#SingleInstance Force
#Persistent
SetTimer, Check, 250         ;set the timer. TODO: figure out how to get the parameter in here
return

; timer check
Check:
If (A_TimeIdle>=IDLE_DURATION)
{
  setBacklight(0)
  ; SetTimer, Check, Off
}
else
{
  setBacklight(1)
}
return

; set backlight on or off
setBacklight(isOn)
{
  ; do not call backlight program if level is already set
  if (wasOn != isOn){
    wasOn := isOn
    level := isOn ? BACKLIGHT_LEVEL : 0
    Run, thinkpadlightv02.exe "c:\Users\All Users\Lenovo\ImController\Plugins\ThinkKeyboardPlugin\x86\Keyboard_Core.dll" %level% , , Hide
  }
}
