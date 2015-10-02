// include Fake lib
#r @"../packages/FAKE/tools/FakeLib.dll"

open System.IO
open Fake

RestorePackages()

#load "./config.fsx"
#load "./test.fsx"

open Config

Target "Help" (fun _ ->
    PrintTargets()
    printf "\nDefault Build:Debug\n\n"
)

let addBuildTarget name env sln =
    let rebuild config = {(setParams config) with Targets = ["Build"]}
    Target (targetWithEnv name env) (fun _ ->
        setBuildMode env
        build rebuild sln
    )

environments |> Seq.iter (fun env -> 
    addBuildTarget "Build" env mainSln
)


"Build:Debug"
  ==> "Canopy"

// start build
RunTargetOrDefault (targetWithEnv "Build" "Debug")