Imports System.Runtime.InteropServices

Public Class KeepAlive
    'Approach I: Inhibit Sleep mode

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Protected Shared Function SetThreadExecutionState(ByVal esFlags As EXECUTION_STATE) As EXECUTION_STATE
    End Function

    <FlagsAttribute>
    Public Enum EXECUTION_STATE As Integer
        ES_AWAYMODE_REQUIRED = &H40
        ES_CONTINUOUS = &H80000000
        ES_DISPLAY_REQUIRED = &H2
        ES_SYSTEM_REQUIRED = &H1
    End Enum



    'Approach II: Use SendInput to send a mouse move

    Public Declare Function SendInput Lib "user32" (ByVal nInputs As Integer, ByRef pInputs As INPUT, ByVal cbSize As Integer) As Integer

    Public Structure INPUT
        Enum InputType As Integer
            INPUT_MOUSE = 0
            INPUT_KEYBOARD = 1
            INPUT_HARDWARE = 2
        End Enum

        Dim dwType As InputType
        Dim mkhi As MOUSEKEYBDHARDWAREINPUT
    End Structure

    Public Structure MOUSEINPUT
        Enum MouseEventFlags As Integer
            MOUSEEVENTF_MOVE = &H1
            MOUSEEVENTF_LEFTDOWN = &H2
            MOUSEEVENTF_LEFTUP = &H4
            MOUSEEVENTF_RIGHTDOWN = &H8
            MOUSEEVENTF_RIGHTUP = &H10
            MOUSEEVENTF_MIDDLEDOWN = &H20
            MOUSEEVENTF_MIDDLEUP = &H40
            MOUSEEVENTF_XDOWN = &H80
            MOUSEEVENTF_XUP = &H100
            MOUSEEVENTF_WHEEL = &H800
            MOUSEEVENTF_VIRTUALDESK = &H4000
            MOUSEEVENTF_ABSOLUTE = &H8000
        End Enum

        Dim dx As Integer
        Dim dy As Integer
        Dim mouseData As Integer
        Dim dwFlags As MouseEventFlags
        Dim time As Integer
        Dim dwExtraInfo As IntPtr
    End Structure

    Public Structure KEYBDINPUT
        Public wVk As Short
        Public wScan As Short
        Public dwFlags As Integer
        Public time As Integer
        Public dwExtraInfo As IntPtr
    End Structure

    Public Structure HARDWAREINPUT
        Public uMsg As Integer
        Public wParamL As Short
        Public wParamH As Short
    End Structure

    Const KEYEVENTF_EXTENDEDKEY As UInt32 = &H1
    Const KEYEVENTF_KEYUP As UInt32 = &H2
    Const KEYEVENTF_UNICODE As UInt32 = &H4
    Const KEYEVENTF_SCANCODE As UInt32 = &H8
    Const XBUTTON1 As UInt32 = &H1
    Const XBUTTON2 As UInt32 = &H2

    <StructLayout(LayoutKind.Explicit)> Public Structure MOUSEKEYBDHARDWAREINPUT
        <FieldOffset(0)> Public mi As MOUSEINPUT
        <FieldOffset(0)> Public ki As KEYBDINPUT
        <FieldOffset(0)> Public hi As HARDWAREINPUT
    End Structure


    Public Sub MoveMouseViaSendInput()
        Dim i(0) As INPUT
        i(0).dwType = INPUT.InputType.INPUT_MOUSE
        i(0).mkhi = New MOUSEKEYBDHARDWAREINPUT With {.mi = New MOUSEINPUT}
        i(0).mkhi.mi.dx = 0
        i(0).mkhi.mi.dy = 0
        i(0).mkhi.mi.mouseData = 0
        i(0).mkhi.mi.dwFlags = MOUSEINPUT.MouseEventFlags.MOUSEEVENTF_MOVE
        i(0).mkhi.mi.time = 0
        i(0).mkhi.mi.dwExtraInfo = IntPtr.Zero
        SendInput(1, i(0), Marshal.SizeOf(i(0)))
    End Sub

    Public Shared Sub Main()
        Dim Ego As New KeepAlive()

        If Environment.CommandLine.ToLower.Contains("/nosleep") Then
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS Or EXECUTION_STATE.ES_AWAYMODE_REQUIRED)
        End If

        Const TIMER As Integer = (1000 * 60 * 5) - 200
        Do
            Threading.Thread.Sleep(TIMER)
            Ego.MoveMouseViaSendInput()
        Loop

    End Sub

End Class
