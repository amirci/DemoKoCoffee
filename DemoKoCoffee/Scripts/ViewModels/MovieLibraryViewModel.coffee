class DemoKoCoffee.MovieLibraryViewModel
  
  constructor: (title) ->
    @title = ko.observable(title)
    @movies = [
      new Movie('Blazing Saddles', new Date(1972, 1, 3)),
      new Movie('Young Frankenstain', new Date(1972, 1, 5)),
      new Movie('Spaceballs', new Date(1980, 1, 3)),
    ]
    @editingTitle = ko.observable(false)
    @newTitle = ko.observable()
    
  editTitle: => 
    @newTitle @title()
    @editingTitle(true)
  
  saveEditing: => 
    @title @newTitle()
    @editingTitle(false)
  
  cancelEditing: => @editingTitle(false)

    
class Movie
  
  constructor: (@title, @relDate) ->
    @releaseDate = $.format.date @relDate, 'MMM dd yyyy'