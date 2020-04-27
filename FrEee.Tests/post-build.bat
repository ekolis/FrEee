@echo off
REM concatenate all arguments except the first with spaces between them
for /f "tokens=1,* delims= " %%a in ("%*") do set ALL_BUT_FIRST=%%b

echo Copying assets to %ALL_BUT_FIRST%\bin\%1\net472

robocopy "%ALL_BUT_FIRST%\..\FrEee\Mods" "%ALL_BUT_FIRST%\%1\Mods" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Data" "%ALL_BUT_FIRST%\%1\Data" /e
robocopy "%ALL_BUT_FIRST%\Mods" "%ALL_BUT_FIRST%\%1\Mods" /e
robocopy "%ALL_BUT_FIRST%\Savegame" "%ALL_BUT_FIRST%\%1\Savegame" /e

echo Done copying assets