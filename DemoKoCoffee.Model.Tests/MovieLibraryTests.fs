module DemoKoCoffee.Model.Tests.MovieLibrary

open FsUnit
open NUnit.Framework

[<Test>]
let ``Insert adds a new object to the library`` () =
    "ships" |> should startWith "sh"