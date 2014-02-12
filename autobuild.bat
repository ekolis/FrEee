REM this script will build FrEee, assuming you have the .NET Framework v4.0 installed
REM if you create the following lines (without REM) in your .hg/hgrc file then it will automatically run when you update (handy when pulling other developers' changes)
REM
REM [hooks]
REM update = autobuild.bat
REM
REM or, you can just run the script yourself as needed if you don't want to be bothered firing up Visual Studio

C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe FrEee.sln