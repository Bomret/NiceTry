// include Fake lib
#r @"tools\FAKE\tools\FakeLib.dll"
open Fake 

RestorePackages()

let buildDir = "./build"
let testDir = "./test"
let testAssemblies = !! (testDir + "/*.Tests.dll")

Target "Clean" (fun _ -> CleanDirs [buildDir; testDir])

Target "BuildLib" (fun _ -> 
    !! "NiceTry/**/*.csproj"
    |> MSBuildRelease buildDir "Build"
    |> Log "Build output: "
)

Target "BuildTests" (fun _ -> 
    !! "NiceTry.Tests/**/*.csproj"
    |> MSBuildRelease testDir "Build"
    |> Log "Build output: "
)

Target "Test" (fun _ ->
    testAssemblies
        |> MSpec (fun p -> 
            {p with 
                ExcludeTags = ["LongRunning"]
                HtmlOutputDir = testDir})
)

"Clean"
    ==> "BuildLib"
    ==> "BuildTests"
    ==> "Test"

// start build
RunTargetOrDefault "Test"