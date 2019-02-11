@echo off
REM concatenate all arguments except the first with spaces between them
for /f "tokens=1,* delims= " %%a in ("%*") do set ALL_BUT_FIRST=%%b

echo Copying assets to %ALL_BUT_FIRST%\bin\%1\net472

robocopy "%ALL_BUT_FIRST%\..\FrEee\Mods" "%ALL_BUT_FIRST%\bin\%1\Mods" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Data" "%ALL_BUT_FIRST%\bin\%1\Data" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\GameSetups" "%ALL_BUT_FIRST%\bin\%1\GameSetups" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Docs" "%ALL_BUT_FIRST%\bin\%1\Docs" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Music" "%ALL_BUT_FIRST%\bin\%1\Music" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Pictures" "%ALL_BUT_FIRST%\bin\%1\Pictures" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Licenses" "%ALL_BUT_FIRST%\bin\%1\Licenses" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Scripts" "%ALL_BUT_FIRST%\bin\%1\Scripts" /e
robocopy "%ALL_BUT_FIRST%\..\FrEee\Dsgnname" "%ALL_BUT_FIRST%\bin\%1\Dsgnname" /e

echo Done copying assets