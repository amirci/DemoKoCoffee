namespace DemoKoCoffee.Acceptance
open NUnit.Framework
open FsUnit
open canopy
open System
open DemoKoCoffee.Model

[<TestFixture>]
module ``Movie list tests`` =

    [<SetUp>]
    let beforeTest =
        // start an instance of the browser
        canopy.configuration.phantomJSDir <- @".\"
        start phantomJS
        // start firefox
        
    [<TearDown>]
    let afterTest = quit()

    let clearMovies = MovieRepository().Clear

    let openMovieListPage = fun _ ->
        // When I see the list of movies
        url "http://localhost:1419/"

        click "Demo binding"


    type ``When the movie database is empty``() = 

        [<SetUp>]
        let setUp = clearMovies >> openMovieListPage

        [<Test>]
        member this.``the default movies are loaded``() = 
            let elementText = fun (e: OpenQA.Selenium.IWebElement) -> e.Text
            let expected = MovieRepository.DefaultMovies |> Seq.map(fun m -> m.Title)
            let actual = (elements ".movie") |> Seq.map(elementWithin ".name" >> elementText)
            actual |> should equal expected

