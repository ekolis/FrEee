nuget restore -PackagesDirectory "../packages"

xbuild FrEee*.csproj 2>&1 |tee xbuild.log
