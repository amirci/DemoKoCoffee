namespace DemoKoCoffee.Acceptance

open NUnit.Framework
open canopy
open runner
open System

[<TestFixture>]
type TestClass() = 

    [<SetUp>]
    member this.beforeTest() =
        // start an instance of the firefox browser
        start firefox

    [<TearDown>]
    member this.afterTest() =
        quit()

    [<Test>]
    member this.When2IsAddedTo2Expect4() = 
        Assert.AreEqual(4, 2+2)


    [<Test>]
    member this.WhenCheckingGithub() = 
        // go to url
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