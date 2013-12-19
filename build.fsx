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
    |> MSBuildDebug testDir "Build"
    |> Log "Test build output: "
)

Target "Test" (fun _ ->
    testAssemblies
        |> MSpec (fun p -> {p with HtmlOutputDir = testDir})
)

"Clean"
    ==> "BuildLib"
    ==> "BuildTests"
    ==> "Test"

RunTargetOrDefault "Test"