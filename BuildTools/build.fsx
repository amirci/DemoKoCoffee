// include Fake lib
#r @"../packages/FAKE/tools/FakeLib.dll"

open System.IO
open Fake

RestorePackages()

#load "./config.fsx"
#load "./test.fsx"

open Config

Target "Help" PrintTargets

let addBuildTarget name env sln =
    let rebuild config = {(setParams config) with Targets = ["Rebuild"]}
    Target (targetWithEnv name env) (fun _ ->
        setBuildMode env
        build rebuild sln
    )

environments |> Seq.iter (fun env -> 
    addBuildTarget prjName env mainSln

    Target  (targetWithEnv "All" env) (fun _ ->
        run (targetWithEnv prjName env)
    )
)



// start build
RunTargetOrDefault "All:Debug"