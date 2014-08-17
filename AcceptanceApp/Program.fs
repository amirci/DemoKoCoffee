open canopy
open runner
open System
open NUnit.Framework
open FsUnit
open DemoKoCoffee.Model

canopy.configuration.phantomJSDir <- @".\"
start phantomJS
// start firefox

let elementText = fun (e: OpenQA.Selenium.IWebElement) -> e.Text

let openMainPage = fun _ ->
    url "http://localhost:1419/"
    click "Demo binding"

let repo = MovieRepository()

let clearMovies = repo.Clear

let actualMovies = fun _ -> (elements ".movie .name") |> Seq.map(elementText)

let defaultMovies = MovieRepository.DefaultMovies |> Seq.map(fun m -> m.Title)

context "When the database is empty"

// Given I have no movies
// When I open the list of movies
before(clearMovies >> openMainPage)

"the default movies are listed" &&& fun _ ->
    actualMovies() |> should equal defaultMovies


context "When the database has movies stored"

let storeNewMovies = fun _ ->
    let makeMovie t rd = Movie(Title=t, ReleaseDate=rd)
    let stored = [(makeMovie "Jaws" "Jan 1, 1990") ; (makeMovie "Aliens" "Jan 1, 2003")]
    stored |> Seq.iter(repo.Save)

before(clearMovies >> storeNewMovies >> openMainPage)

"all the new movies are listed" &&& fun _ ->
    let expected = ["Jaws"; "Aliens"]
    actualMovies() |> should equal expected


context "When adding a movie to the list"

let addNewMovie = fun _ ->
    click ".your-movies button"
    ".new-movie input" << "High Anxiety"
    (last ".new-movie input") << "Sep 1, 1978"

context "and is saved"
before(clearMovies >> openMainPage >> addNewMovie >> (fun _ -> click ".icon-ok-sign"))
"the new movie is listed" &&& fun _ ->
    let expected = Seq.append defaultMovies ["High Anxiety"]
    actualMovies() |> should equal expected

context "and is canceled"
before(clearMovies >> openMainPage >> addNewMovie >> (fun _ -> click ".icon-ban-circle"))
"the new movie is not listed" &&& fun _ ->
    actualMovies() |> should equal defaultMovies


//run all tests
run()

printfn "press [enter] to exit"
System.Console.ReadLine() |> ignore

quit()