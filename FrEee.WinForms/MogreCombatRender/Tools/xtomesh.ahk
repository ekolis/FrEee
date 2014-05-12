#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.
SetTitleMatchMode 1
FileSelectFile, deled_executible, 3, , Select DeleD, Select DeleD.exe (deled.exe; DeleD.lnk)
if deled_executible =
    {
        MsgBox, You didn't select anything.
        Exit
    }

FileSelectFolder, importFolder, , 0, Select ImportFolder
if importFolder =
    {
        MsgBox, You didn't select a folder. 
        Exit
    }

FileSelectFolder, exportFolder, , 3, Select ExportFolder
if exportFolder =
    {
        MsgBox, You didn't select a folder.
        Exit
    }
  

ArrayCount=0    
Loop, %importFolder%\*.x, 0, 0  ; Recurse into subfolders.
{
    ArrayCount += 1
    xfilesArray%ArrayCount% := A_LoopFileName
}

Run "%deled_executible%"

WinWait DeleD 
WinActivate DeleD 
WinWaitActive DeleD 

Loop %ArrayCount%
{

    xfile := xfilesArray%A_Index%
    xfilepath = %importFolder%\%xfile%  
    StringTrimRight, meshfile, xfile, 2
    meshfilepath = %exportFolder%\%meshfile%.mesh
    FoundPos := RegExMatch(meshfile, "_")

    pfix := SubStr(meshfile, 1, FoundPos-1)
    

    WinActivate DeleD 
    WinWaitActive DeleD 
    Send ^n
    
    Send {ALT down} 
    Send p 
    Send e
    Send {ALT up}

    WinWaitActive DirectX X Importer for DeleD 3D Editor
    ControlFocus, TEdit2, DirectX X Importer for DeleD 3D Editor
    ControlSetText, TEdit2, %xfilepath%, DirectX X Importer for DeleD 3D Editor
    
    Send {Enter}
    
    WinWaitActive DeleD 
    
    
    
    Send {ALT down} 
    Send p 
    Send o
    Send {ALT up}
    
    WinWaitActive Ogre Mesh Exporter 
    ControlFocus, TEdit2, Ogre Mesh Exporter 
    ControlSetText, TEdit2, %meshfilepath%, Ogre Mesh Exporter 
    ControlSetText, TEdit1, %meshfile%, Ogre Mesh Exporter 
    ControlClick, TPanel6, Ogre Mesh Exporter, OK
    
    FileAppend,
    (
    {
    "MainMesh": {
        "Name" : "%meshfile%.mesh",
        "Scale" : [1.0,1.0,1.0]
    },
    }
    ), %exportFolder%\%meshfile%.cfg
}

    


