(function() {
  var Movie;

  DemoKoCoffee.MovieLibraryViewModel = (function() {
    function MovieLibraryViewModel(title) {
      this.title = title;
      this.movies = [new Movie('Blazing Saddles', new Date(1972, 1, 3)), new Movie('Young Frankenstain', new Date(1972, 1, 5)), new Movie('Spaceballs', new Date(1980, 1, 3))];
    }

    return MovieLibraryViewModel;

  })();

  Movie = (function() {
    function Movie(title, relDate) {
      this.title = title;
      this.relDate = relDate;
      this.releaseDate = $.format.date(this.relDate, 'MMM dd yyyy');
    }

    return Movie;

  })();

}).call(this);
