#define WINVER 0x0500
#include <windows.h>
//#include <commctrl.h>
#include <stdio.h>
//#include "resource.h"
#include <iostream>
//#include "powrprof.h"


int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nShowCmd)
{
    INPUT ip;
    ip.type = INPUT_MOUSE;
    ip.mi.dx=0;
    ip.mi.dy=0;
    ip.mi.mouseData=0;
    ip.mi.dwFlags=MOUSEEVENTF_MOVE;
    ip.mi.time=0;

    const DWORD SleepTime = (1000 * 60 * 5)-500;

    do {
            SendInput(1, &ip, sizeof(INPUT));
            Sleep(SleepTime);
    }while(1 == 1);


    return 0;
}
