# TODO: .NET core can pick *.bat or *.sh on build
# building could be moved to use that facility

nuget restore -PackagesDirectory "../packages"

# Prevent post-build events (which is a batch file) from running

xbuild FrEee*.csproj /p:PostBuildEvent= 2>&1 |tee xbuild.log

# Do the post-build stuff on Linux here

./post-build.sh
