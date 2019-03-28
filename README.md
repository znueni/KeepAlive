# KeepAlive
Small tool to keep Windows from starting Screensaver / entering sleep mode

Precompiled binary, ready to use is in the BIN folder.

Code is VB.NET, compiles with any .NET version. Its simply a few Windows API calls.

# How to use it
Run the application. 
That's it. Nothing else. The app wont create any visible window, you will not see anything.
But as long as it runs, your system will never start the screensaver or go into an inactivity-based sleep mode.

To stop the application, open your Task Manager and kill it.


# How it works

It uses the Windows SendInput API to tell the system to move the mouse by zero pixels every five minutes (actually every 4.9 minutes).
For windows, thats the same effect as if you jiggle your mouse every five minutes :)


# Prevent Sleep
You can start the app with the /nosleep parameter. It then will also prevent the system from entering Sleep/Standby as long as the application is running.
This can be pretty annoying, because even if you deliberately send the system to standby, it will not accept it. It simply cannot go into Standby anymore.
So, by default, this function is disabled and has to be enabled with the command line switch.
