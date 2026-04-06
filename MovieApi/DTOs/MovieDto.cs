namespace MovieApi.DTOs
{
    public class MovieDto
    {
        public string MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string imdbID { get; set; }
        public string PosterUrl { get; set; }
    }
}
