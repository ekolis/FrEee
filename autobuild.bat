REM this script will build FrEee, assuming you have the .NET Framework 4.6 and MSBuild installed
REM .NET 4.6: https://www.microsoft.com/en-us/download/details.aspx?id=48130
REM MSBuild: https://go.microsoft.com/fwlink/?LinkId=615458
REM
REM if you create the following lines (without REM) in your .hg/hgrc file then it will automatically run when you update (handy when pulling other developers' changes)
REM
REM [hooks]
REM update = autobuild.bat
REM
REM or, you can just run the script yourself as needed if you don't want to be bothered firing up (or installing!) Visual Studio
REM
REM before running this script you might first want to run clone-dependencies.bat (or pull-update-dependencies.bat if you have already cloned them)
REM using those scripts will require you have Mercurial installed
REM my favorite Mercurial client: http://tortoisehg.bitbucket.org/

if exist "C:\Program Files\MSBuild\14.0\Bin\msbuild.exe" ("C:\Program Files\MSBuild\14.0\Bin\msbuild.exe" FrEee.sln) else if exist "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" ("C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" FrEee.sln) else echo MSBuild is not installed! Download it from https://go.microsoft.com/fwlink/?LinkId=615458