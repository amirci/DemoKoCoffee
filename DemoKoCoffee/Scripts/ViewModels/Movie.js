(function() {

  DemoKoCoffee.Movie = (function() {

    function Movie(json) {
      var _this = this;
      ko.mapping.fromJS(json, {}, this);
      this.releaseDate = ko.computed(function() {
        return $.format.date(new Date(_this.releaseDate()), 'MMM dd yyyy');
      });
    }

    Movie.all = function(options) {
      var _this = this;
      return $.ajax({
        type: 'GET',
        url: '/movies',
        success: function(data) {
          var m;
          return options.success((function() {
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
        error: options.error
      });
    };

    return Movie;

  })();

}).call(this);
