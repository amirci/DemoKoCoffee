class DemoKoCoffee.MovieLibraryViewModel
  
  constructor: (title) ->
    @title = ko.observable(title)
    @movies = ko.observableArray()
    @editingTitle = ko.observable(false)
    @newTitle = ko.observable()
    @count = ko.computed => @movies().length
    @newMovieVM = new NewMovieViewModel(@movies)
    @loadMovies()
    
  loadMovies: =>
    DemoKoCoffee.Movie.all
      success: @movies
      error: -> console.log "Error loading movies!"
        
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
    @active = ko.observable false
    @title = ''
    @relDate = ''
    @active.subscribe @activate
    
  activate: (active) => 
    @relDate = @title = '' if active

  save: =>
    @active false
    DemoKoCoffee.Movie.create
      movie:
        title: @title
        releaseDate: @relDate
      success: (m) =>
        @movies.push m
      error: -> console.log "An error ocurred trying to add the movie"
    
  cancel: => @active false
    
