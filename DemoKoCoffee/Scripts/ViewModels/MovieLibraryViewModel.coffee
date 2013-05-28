class DemoKoCoffee.MovieLibraryViewModel
  
  constructor: (@title) ->
    @movies = [
      new Movie('Blazing Saddles', new Date(1972, 1, 3)),
      new Movie('Young Frankenstain', new Date(1972, 1, 5)),
      new Movie('Spaceballs', new Date(1980, 1, 3)),
    ]
    
    
    
class Movie
  
  constructor: (@title, @relDate) ->
    @releaseDate = $.format.date @relDate, 'MMM dd yyyy'