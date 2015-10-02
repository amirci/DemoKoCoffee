using MongoDB.Bson;

namespace DemoKoCoffee.Model
{
    public class Movie
    {
        public string Title       { get; set; }
        public string ReleaseDate { get; set; }
        public ObjectId Id { get; set; }
    }
}
