namespace DemoKoCoffee.Acceptance

open NUnit.Framework
open canopy
open runner
open System
open DemoKoCoffee.Model

[<TestFixture>]
type ``Adding movies to the list``() = 

    [<SetUp>]
    member this.beforeTest() =
        // start an instance of the browser
        canopy.configuration.phantomJSDir <- @".\"
        start phantomJS
        // start firefox

    [<TearDown>]
    member this.afterTest() =
        quit()

    [<Test>]
    member this.``When the movie database is empty``() = 
        // Given I have no movies
        MovieRepository().Clear()

        // When I see the list of movies
        url "http://localhost:1419/"

        click "Demo binding"

        // Then all the default movies are loaded
        do
            let elementText = fun (e: OpenQA.Selenium.IWebElement) -> e.Text
            let expected = MovieRepository.DefaultMovies |> Seq.map(fun m -> m.Title)
            let actual = (elements ".movie") |> Seq.map(fun e -> elementWithin ".name" e |> elementText)
            Assert.That(actual, Is.EquivalentTo expected)

