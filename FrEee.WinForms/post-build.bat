@echo off
REM concatenate all arguments except the first with spaces between them
for /f "tokens=1,* delims= " %%a in ("%*") do set ALL_BUT_FIRST=%%b

echo Purging hardcoded DLL references from scripts

CD "%ALL_BUT_FIRST%\%1\Scripts"
For /R %%G in ("*.csx") do (
echo %%~fG.temp
echo %%G
ren "%%~fG" "%%~nxG.temp"
findstr /V "../../../bin/Debug/FrEee.Core.dll" "%%G.temp" > "%%~fG"
del "%%G.temp"
)


