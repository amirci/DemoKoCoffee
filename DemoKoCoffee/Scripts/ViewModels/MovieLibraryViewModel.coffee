class DemoKoCoffee.MovieLibraryViewModel
  
  constructor: (@title) ->
    @movies = [
      new Movie('Blazing Saddles', new Date(1972, 1, 3)),
      new Movie('Young Frankenstain', new Date(1972, 1, 5)),
      new Movie('Spaceballs', new Date(1980, 1, 3)),
    ]
    @editingTitle = false
    
  editTitle: =>
    @editingTitle = true
    console.log "Editing title #{@editingTitle}"
    
class Movie
  
  constructor: (@title, @relDate) ->
    @releaseDate = $.format.date @relDate, 'MMM dd yyyy'