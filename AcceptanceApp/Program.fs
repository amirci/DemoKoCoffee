open canopy
open runner
open System
open NUnit.Framework
open FsUnit
open DemoKoCoffee.Model
open FSharpx

canopy.configuration.phantomJSDir <- @".\"
//start phantomJS
start firefox

let elementText = fun (e: OpenQA.Selenium.IWebElement) -> e.Text

let openMainPage = fun _ ->
    url "http://localhost:9099/"
    click "Demo binding"

let repo = MovieRepository()

let clearMovies = repo.Clear

let actualMovies () =
  someElement ".movie .name"
  |> Option.map (fun _ -> elements ".movie .name" |> List.map elementText) 
  |> Option.getOrElse List.empty

let defaultMovies = 
    MovieRepository.DefaultMovies 
    |> Seq.map (fun m -> m.Title)
    |> List.ofSeq

context "When the database is empty"
before (clearMovies >> openMainPage)

"No movies are listed" &&& fun _ ->
    actualMovies() 
    |> should equal List.empty


context "When the database has movies stored"
let storeNewMovies = fun _ ->
//    let makeMovie t rd = Movie(Title=t, ReleaseDate=rd)
//    let stored = [(makeMovie "Jaws" "Jan 1, 1990") ; (makeMovie "Aliens" "Jan 1, 2003")]
    MovieRepository.DefaultMovies 
    |> Seq.iter repo.Save

before (clearMovies >> storeNewMovies >> openMainPage)

"all stored movies are listed" &&&& fun _ ->
    actualMovies()
    |> should equal defaultMovies


context "When adding a movie to the list and is saved"
let saveNewMovie = fun _ -> click ".icon-ok-sign"

let addNewMovie = fun _ ->
    click ".your-movies button"
    ".new-movie input" << "High Anxiety"
    (last ".new-movie input") << "Sep 1, 1978"

before(clearMovies >> openMainPage >> addNewMovie >> saveNewMovie)
"the new movie is listed" &&& fun _ ->
    let expected = Seq.append defaultMovies ["High Anxiety"]
    actualMovies() |> should equal expected
    notDisplayed ".new-movie"


context "When adding a movie to the list and is canceled"
let cancelOperation = fun _ -> click ".icon-ban-circle"
before(clearMovies >> openMainPage >> addNewMovie >> cancelOperation)
"the new movie is not listed" &&& fun _ ->
    actualMovies() |> should equal defaultMovies
    notDisplayed ".new-movie"

//run all tests
run()

printfn "press [enter] to exit"
System.Console.ReadLine() |> ignore

quit()