@echo off
cls
".nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "." "-ExcludeVersion"
"tools\FAKE\tools\Fake.exe" build.fsx
pause