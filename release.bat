if exist "C:\Program Files\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe" (
	("C:\Program Files\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe" FrEee.sln /p:Configuration=Release)
) else if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe" (
	("C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe" FrEee.sln /p:Configuration=Release)
) else (
	echo MSBuild v16 is not installed! Download it from https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=BuildTools&rel=16 please.
)
cd FrEee.WinForms\bin
ren Release FrEee
del FrEee.7z
"C:\Program Files\7-zip\7z.exe" a FrEee.7z FrEee
ren FrEee Release
copy FrEee.7z "%UserProfile%\Google Drive\FrEee Builds\"