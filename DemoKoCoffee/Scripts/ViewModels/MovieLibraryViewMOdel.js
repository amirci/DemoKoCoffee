(function() {
  var Movie, NewMovieViewModel,
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
      var _this = this;
      return $.ajax({
        type: 'GET',
        url: '/movies',
        success: function(data) {
          var m;
          return _this.movies((function() {
            var _i, _len, _ref, _results;
            _ref = data.movies;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
              m = _ref[_i];
              _results.push(new Movie(m));
            }
            return _results;
          })());
        },
        error: function() {
          return console.log("Error calling /movies!");
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
      this.active(false);
      return this.movies.push(new Movie({
        title: this.title,
        releaseDate: new Date(this.relDate)
      }));
    };

    NewMovieViewModel.prototype.cancel = function() {
      return this.active(false);
    };

    return NewMovieViewModel;

  })();

  Movie = (function() {

    function Movie(json) {
      var _this = this;
      ko.mapping.fromJS(json, {}, this);
      this.releaseDate = ko.computed(function() {
        return $.format.date(new Date(_this.releaseDate()), 'MMM dd yyyy');
      });
    }

    return Movie;

  })();

}).call(this);
