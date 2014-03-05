#r @"FAKE\tools\FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

RestorePackages()

let buildDir = "./build"
let testDir = "./test"
let testAssemblies = !! (testDir + "/*.Tests.dll")
let version = 
    match buildServer with
        | TeamCity -> buildVersion
        | _ -> "2.1.0"

Target "Clean" (fun _ -> CleanDirs [buildDir; testDir])

Target "BuildLib" (fun _ -> 
    CreateCSharpAssemblyInfo "./NiceTry/Properties/AssemblyInfo.cs"
        [Attribute.Title "NiceTry"
         Attribute.Description "A functional wrapper type for the classic try/catch statement"
         Attribute.Guid "d9712c70-9a11-43b9-b9b4-10b4036ea8f2"
         Attribute.Product "NiceTry"
         Attribute.Version version
         Attribute.FileVersion version]

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