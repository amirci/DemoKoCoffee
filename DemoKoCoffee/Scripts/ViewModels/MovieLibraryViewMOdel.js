(function() {

  DemoKoCoffee.MovieLibraryViewModel = (function() {

    function MovieLibraryViewModel(title) {
      this.title = title;
      console.log(">>>>> Calling the viewmodel");
    }

    return MovieLibraryViewModel;

  })();

}).call(this);
