module DemoKoCoffee.Model.Tests.MovieLibrary

open FsUnit
open NUnit.Framework
open DemoKoCoffee.Model

let repo = new MovieRepository("MovieRepoTest")
let makeMovie t rd = Movie(Title=t, ReleaseDate=rd)

[<SetUp>]
let ``Before each test`` () =
    repo.Clear()    

[<Test>]
let ``Clear removes all the movies`` () =
    MovieRepository.DefaultMovies
    |> Seq.iter repo.Save

    repo.Count |> should equal (MovieRepository.DefaultMovies |> Seq.length)

    repo.Clear()

    repo.IsEmpty |> should equal true

[<Test>]
let ``Save adds movies to the library`` () =
    let stored = [
        makeMovie "Aliens" "Jan 1, 2003"
        makeMovie "Jaws"   "Jan 1, 1990"
    ]
    
    stored |> List.iter repo.Save

    repo.All()
    |> Seq.map (fun m -> m.Title, m.ReleaseDate)
    |> should equal [ "Aliens", "Jan 1, 2003"; "Jaws", "Jan 1, 1990" ]


[<Test>]
let ``IsEmpty checks the repo is empty`` () =
    repo.IsEmpty |> should equal true