@echo off
REM concatenate all arguments except the first with spaces between them
for /f "tokens=1,* delims= " %%a in ("%*") do set ALL_BUT_FIRST=%%b

echo Copying assets to %ALL_BUT_FIRST%\%1

robocopy "%ALL_BUT_FIRST%\..\FrEee\Mods" "%ALL_BUT_FIRST%\%1\Mods" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Data" "%ALL_BUT_FIRST%\%1\Data" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\GameSetups" "%ALL_BUT_FIRST%\%1\GameSetups" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Docs" "%ALL_BUT_FIRST%\%1\Docs" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Music" "%ALL_BUT_FIRST%\%1\Music" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Pictures" "%ALL_BUT_FIRST%\%1\Pictures" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Licenses" "%ALL_BUT_FIRST%\%1\Licenses" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Scripts" "%ALL_BUT_FIRST%\%1\Scripts" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Dsgnname" "%ALL_BUT_FIRST%\%1\Dsgnname" /e

echo Done copying assets

CD "%ALL_BUT_FIRST%\bin\%1\Scripts"
For /R %%G in ("*.csx") do (
echo %%~fG.temp
echo %%G
ren "%%~fG" "%%~nxG.temp"
findstr /V "../../../bin/Debug/FrEee.Core.dll" "%%G.temp" > "%%~fG"
del "%%G.temp"
)


