(function() {
  var Movie,
    __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

  DemoKoCoffee.MovieLibraryViewModel = (function() {
    function MovieLibraryViewModel(title) {
      this.title = title;
      this.editTitle = __bind(this.editTitle, this);
      this.movies = [new Movie('Blazing Saddles', new Date(1972, 1, 3)), new Movie('Young Frankenstain', new Date(1972, 1, 5)), new Movie('Spaceballs', new Date(1980, 1, 3))];
      this.editingTitle = ko.observable(false);
    }

    MovieLibraryViewModel.prototype.editTitle = function() {
      return this.editingTitle(true);
    };

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
