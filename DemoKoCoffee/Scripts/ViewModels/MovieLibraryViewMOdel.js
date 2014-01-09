(function() {
  var NewMovieViewModel,
    __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

  DemoKoCoffee.MovieLibraryViewModel = (function() {

    function MovieLibraryViewModel(title) {
      this.cancelEditing = __bind(this.cancelEditing, this);

      this.saveEditing = __bind(this.saveEditing, this);

      this.editTitle = __bind(this.editTitle, this);

      this.newMovie = __bind(this.newMovie, this);

      this.loadMovies = __bind(this.loadMovies, this);

      var _this = this;
      this.title = ko.observable(title);
      this.movies = ko.observableArray();
      this.editingTitle = ko.observable(false);
      this.newTitle = ko.observable();
      this.count = ko.computed(function() {
        return _this.movies().length;
      });
      this.newMovieVM = new NewMovieViewModel(this.movies);
      this.loadMovies();
    }

    MovieLibraryViewModel.prototype.loadMovies = function() {
      return DemoKoCoffee.Movie.all({
        success: this.movies,
        error: function() {
          return console.log("Error loading movies!");
        }
      });
    };

    MovieLibraryViewModel.prototype.newMovie = function() {
      return this.newMovieVM.active(true);
    };

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

  NewMovieViewModel = (function() {

    function NewMovieViewModel(movies) {
      this.movies = movies;
      this.cancel = __bind(this.cancel, this);

      this.save = __bind(this.save, this);

      this.activate = __bind(this.activate, this);

      this.active = ko.observable(false);
      this.title = '';
      this.relDate = '';
      this.active.subscribe(this.activate);
    }

    NewMovieViewModel.prototype.activate = function(active) {
      if (active) {
        return this.relDate = this.title = '';
      }
    };

    NewMovieViewModel.prototype.save = function() {
      var _this = this;
      this.active(false);
      return DemoKoCoffee.Movie.create({
        movie: {
          title: this.title,
          releaseDate: this.relDate
        },
        success: function(m) {
          return _this.movies.push(m);
        },
        error: function() {
          return console.log("An error ocurred trying to add the movie");
        }
      });
    };

    NewMovieViewModel.prototype.cancel = function() {
      return this.active(false);
    };

    return NewMovieViewModel;

  })();

}).call(this);
