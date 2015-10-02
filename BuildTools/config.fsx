// include Fake lib
#r @"../packages/FAKE/tools/FakeLib.dll"

open System.IO
open System.Xml.XPath
open System.Xml
open Fake

// Properties
[<AutoOpen>]
module Config =
    let testDir     = "./"
    let srcDir      = "./"
    let prjName     = "DemoKoCoffee"
    let mainSln     = prjName + ".sln"
    let mainTestPrj = prjName + ".Tests"
    let mainPrj     = prjName
    let mainConfig  = mainPrj @@ "app.config"
    let testConfig  = mainTestPrj @@ "app.config"

    let environments = ["Debug"; "Release"; "CI"]
    let buildMode () = getBuildParamOrDefault "buildMode" "Release"
    let version      = "1.0.0.0"
    let targetWithEnv target env = sprintf "%s:%s" target env

    let setBuildMode = setEnvironVar "buildMode"

    let debugMode   () = setBuildMode "Debug"
    let releaseMode () = setBuildMode "Release"
    let ciMode () = setBuildMode "CI"

    let setParams defaults =
        { defaults with
            Targets = ["Build"]
            Properties =
                [
                    "Optimize", "True"
                    "Platform", "Any CPU"
                    "Configuration", buildMode()
                ]
        }




