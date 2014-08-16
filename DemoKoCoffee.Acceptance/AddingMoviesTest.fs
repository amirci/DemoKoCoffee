namespace DemoKoCoffee.Acceptance

open NUnit.Framework
open canopy
open runner
open System

[<TestFixture>]
type ``Adding movies to the list``() = 

    [<SetUp>]
    member this.beforeTest() =
        // start an instance of the browser
        canopy.configuration.phantomJSDir <- @".\"
        start phantomJS

    [<TearDown>]
    member this.afterTest() =
        quit()

    [<Test>]
    member this.``When there are movies loaded``() = 
        // Given I have some movies

        // When I see the list of movies
        url "http://localhost:1419/"

        // Then all the movies should be listed


    [<Test>]
    member this.CheckingCanopy() = 
        url "http://lefthandedgoat.github.io/canopy/testpages/"

        //assert that the element with an id of 'welcome' has
        //the text 'Welcome'
        "#welcome" == "Welcome"

        //assert that the element with an id of 'firstName' has the value 'John'
        "#firstName" == "John"

        //change the value of element with
        //an id of 'firstName' to 'Something Else'
        "#firstName" << "Something Else"

        //verify another element's value, click a button,
        //verify the element is updated
        "#button_clicked" == "button not clicked"
        click "#button"
        "#button_clicked" == "button clicked"