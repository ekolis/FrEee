@echo off
REM concatenate all arguments except the first with spaces between them
for /f "tokens=1,* delims= " %%a in ("%*") do set ALL_BUT_FIRST=%%b

if %1 == Release echo Copying assets to %ALL_BUT_FIRST%\bin\Release\net472

if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Mods" "%ALL_BUT_FIRST%\bin\Release\net472\Mods" /e
if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Mods" "%ALL_BUT_FIRST%\bin\Debug\net472\Mods" /e

if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Data" "%ALL_BUT_FIRST%\bin\Release\net472\Data" /e
if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Data" "%ALL_BUT_FIRST%\bin\Debug\net472\Data" /e

if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\GameSetups" "%ALL_BUT_FIRST%\bin\Release\net472\GameSetups" /e
if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\GameSetups" "%ALL_BUT_FIRST%\bin\Debug\net472\GameSetups" /e

if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Docs" "%ALL_BUT_FIRST%\bin\Release\net472\Docs" /e
if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Docs" "%ALL_BUT_FIRST%\bin\Debug\net472\Docs" /e

if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Music" "%ALL_BUT_FIRST%\bin\Release\net472\Music" /e
if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Music" "%ALL_BUT_FIRST%\bin\Debug\net472\Music" /e

if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Pictures" "%ALL_BUT_FIRST%\bin\Release\net472\Pictures" /e
if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Pictures" "%ALL_BUT_FIRST%\bin\Debug\net472\Pictures" /e

if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Licenses" "%ALL_BUT_FIRST%\bin\Release\net472\Licenses" /e
if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Licenses" "%ALL_BUT_FIRST%\bin\Debug\net472\Licenses" /e

if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Scripts" "%ALL_BUT_FIRST%\bin\Release\net472\Scripts" /e
if %1 == Release robocopy "%ALL_BUT_FIRST%\..\FrEee\Scripts" "%ALL_BUT_FIRST%\bin\Debug\net472\Scripts" /e

if %1 == Release echo Done copying assets