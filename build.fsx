#r @"FAKE\tools\FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

RestorePackages()

let buildDir = "./build"
let net451Dir = buildDir + "/net451"
let net45Dir = buildDir + "/net45"
let net40Dir = buildDir + "/net40"
let net35Dir = buildDir + "/net35"

let testDir = "./test"
let packagingDir = "./package"
let testAssemblies = !! (testDir + "/*.Tests.dll")
let version = 
    match buildServer with
        | TeamCity -> buildVersion
        | _ -> "2.1.1"

Target "Clean" (fun _ -> CleanDirs [buildDir; testDir; packagingDir])

Target "BuildLib" (fun _ -> 
    CreateCSharpAssemblyInfo "./NiceTry/Properties/AssemblyInfo.cs"
        [Attribute.Title "NiceTry"
         Attribute.Description "A functional wrapper type for the classic try/catch statement"
         Attribute.Guid "d9712c70-9a11-43b9-b9b4-10b4036ea8f2"
         Attribute.Product "NiceTry"
         Attribute.Version version
         Attribute.FileVersion version]

    !! "NiceTry/**/*.csproj"
    |> MSBuild net451Dir "Build" ["Configuration","Net451"]
    |> Log "Build output: "

    !! "NiceTry/**/*.csproj"
    |> MSBuild net45Dir "Build" ["Configuration","Net45"]
    |> Log "Build output: "

    !! "NiceTry/**/*.csproj"
    |> MSBuild net40Dir "Build" ["Configuration","Net40"]
    |> Log "Build output: "

    !! "NiceTry/**/*.csproj"
    |> MSBuild net35Dir "Build" ["Configuration","Net35"]
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

Target "CreatePackage" (fun _ ->
  CreateDir "package/lib/net451"
  CreateDir "package/lib/net45"
  CreateDir "package/lib/net40"
  CreateDir "package/lib/net35"

  CopyFile "package/lib/net451/NiceTry.dll" "build/net451/NiceTry.dll"
  CopyFile "package/lib/net45/NiceTry.dll" "build/net45/NiceTry.dll"
  CopyFile "package/lib/net40/NiceTry.dll" "build/net40/NiceTry.dll"
  CopyFile "package/lib/net35/NiceTry.dll" "build/net35/NiceTry.dll"

  NuGet (fun p ->
    {p with
        WorkingDir = packagingDir
        OutputPath = packagingDir
        Version = version
        Publish = false
            })
            "NiceTry.nuspec"
)

"Clean"
    ==> "BuildLib"
    ==> "BuildTests"
    ==> "Test"
    ==> "CreatePackage"

RunTargetOrDefault "Test"