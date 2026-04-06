namespace MovieApi.Models
{
    public class FavoriteMovie
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string MovieId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string PosterUrl { get; set; }
    }
}
