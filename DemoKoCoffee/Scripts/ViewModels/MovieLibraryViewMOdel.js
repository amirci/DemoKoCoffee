(function() {
  var Movie,
    __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

  DemoKoCoffee.MovieLibraryViewModel = (function() {
    function MovieLibraryViewModel(title) {
      this.cancelEditing = __bind(this.cancelEditing, this);
      this.saveEditing = __bind(this.saveEditing, this);
      this.editTitle = __bind(this.editTitle, this);      this.title = ko.observable(title);
      this.movies = [new Movie('Blazing Saddles', new Date(1972, 1, 3)), new Movie('Young Frankenstain', new Date(1972, 1, 5)), new Movie('Spaceballs', new Date(1980, 1, 3))];
      this.editingTitle = ko.observable(false);
      this.newTitle = ko.observable();
    }

    MovieLibraryViewModel.prototype.editTitle = function() {
      this.newTitle(this.title());
      return this.editingTitle(true);
    };

    MovieLibraryViewModel.prototype.saveEditing = function() {
      this.title(this.newTitle());
      return this.editingTitle(false);
    };

    MovieLibraryViewModel.prototype.cancelEditing = function() {
      return this.editingTitle(false);
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
