class DemoKoCoffee.Movie

  constructor: (json) ->
    ko.mapping.fromJS(json, {}, this)
    @releaseDate = ko.computed => $.format.date new Date(@releaseDate()), 'MMM dd yyyy'

  @all: (options) ->
    $.ajax 
      type: 'GET'
      url: '/movies'
      success: (data) => options.success(new Movie(m) for m in data.movies)
      error: options.error

  @create: (options) ->
    $.ajax
      type: 'POST'
      data: options.movie
      url: '/movies/create'
      success: (data) => options.success new Movie(data.movie)
      error: options.error