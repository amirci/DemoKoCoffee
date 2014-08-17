namespace DemoKoCoffee.Acceptance

open NUnit.Framework
open FsUnit
open canopy
open System
open DemoKoCoffee.Model

[<TestFixture>]
type ``When the movie database is empty``() = 

    [<SetUp>]
    member this.beforeTest() =
        // start an instance of the browser
        canopy.configuration.phantomJSDir <- @".\"
        start phantomJS
        // start firefox
        
        // Given I have no movies
        MovieRepository().Clear()

        // When I see the list of movies
        url "http://localhost:1419/"

        click "Demo binding"

    [<TearDown>]
    member this.afterTest() =
        quit()

    [<Test>]
    member this.``all the default movies are loaded``() = 
        let elementText = fun (e: OpenQA.Selenium.IWebElement) -> e.Text
        let expected = MovieRepository.DefaultMovies |> Seq.map(fun m -> m.Title)
        let actual = (elements ".movie") |> Seq.map(elementWithin ".name" >> elementText)
        actual |> should equal expected

