@echo off
cls
".nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "." "-ExcludeVersion"
"FAKE\tools\Fake.exe" CreatePackage build.fsx
pause