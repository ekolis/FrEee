#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.
SetTitleMatchMode 1
FileSelectFile, executible, 3, , Select imgmanip, Select imgmanip.exe (imgmanip.exe; imgmanip.lnk)
if executible =
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
Loop, %importFolder%\KpiImp_mini_*.jpg, 0, 0  ; Recurse into subfolders.
{
    ArrayCount += 1
    filesArray%ArrayCount% := A_LoopFileName
}

Loop %ArrayCount%
{
    thisfile := filesArray%A_Index%
    fileinpath = %importFolder%\%thisfile%
    StringTrimRight, outfile, thisfile, 4
    fileoutpath = %exportFolder%\%outfile%.png
    RunWait "%executible%" "%fileinpath%" "128" "128" "0" "%fileoutpath%"
}

    


