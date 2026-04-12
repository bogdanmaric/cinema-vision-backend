namespace MovieApi.DTOs
{
    public class MovieDetailsDto
    {
        public string MovieId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Plot { get; set; }
        public string PosterUrl { get; set; }
    }
}
