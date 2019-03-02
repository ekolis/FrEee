if exist "C:\Program Files\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\msbuild.exe" (
	("C:\Program Files\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\msbuild.exe" FrEee.sln /p:Configuration=Release)
) else if exist "C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\msbuild.exe" (
	("C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\msbuild.exe" FrEee.sln /p:Configuration=Release)
) else (
	echo MSBuild v15 is not installed! Download it from https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=BuildTools^&rel=15#
)
cd FrEee.WinForms\bin
ren Release FrEee
del FrEee.7z
"C:\Program Files\7-zip\7z.exe" a FrEee.7z FrEee
ren FrEee Release
copy FrEee.7z "%UserProfile%\Google Drive\FrEee Builds\"