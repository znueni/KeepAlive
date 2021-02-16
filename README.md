# KeepAlive
Small tool to keep Windows from starting Screensaver / entering sleep mode

Precompiled binary, ready to use is in the BIN folder. [Download here](bin/Keep%20Alive.exe)



# How to use it
Run the application. 
That's it. Nothing else. The app wont create any visible window, you will not see anything.
But as long as it runs, your system will never start the screensaver or go into an inactivity-based sleep mode.

To stop the application, open your Task Manager and kill it.


# How it works

It uses the Windows SendInput API to tell the system to move the mouse by zero pixels every five minutes (actually every 4.9 minutes).
For windows, thats the same effect as if you jiggle your mouse every five minutes :)


