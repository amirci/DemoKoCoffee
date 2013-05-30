class DemoKoCoffee.MovieLibraryViewModel
  
  constructor: (title) ->
    @title = ko.observable(title)
    @movies = ko.observableArray [
      new Movie('Blazing Saddles', new Date(1972, 1, 3)),
      new Movie('Young Frankenstain', new Date(1972, 1, 5)),
      new Movie('Spaceballs', new Date(1980, 1, 3)),
    ]
    @editingTitle = ko.observable(false)
    @newTitle = ko.observable()
    @count = ko.computed => @movies().length
    @newMovieVM = new NewMovieViewModel(@movies)

  newMovie: => @newMovieVM.active(true)
    
  editTitle: => 
    @newTitle @title()
    @editingTitle(true)
  
  saveEditing: => 
    @title @newTitle()
    @editingTitle(false)
  
  cancelEditing: => @editingTitle(false)

class NewMovieViewModel

  constructor: (@movies) ->
    @active = ko.observable(false)
    @title = ''
    @relDate = ''
    
  save: =>
    @active(false)
    @movies.push new Movie(@title, new Date @relDate)
    
  cancel: => @active(false)
    
class Movie

  constructor: (@title='', @relDate=null) ->
    @releaseDate = $.format.date @relDate, 'MMM dd yyyy'