namespace HW4.Models
{
    public class MovieDetails
    {
        public string BackgroundImagePath { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string ReleaseDate { get; set; }
        public List<string> Genres { get; set; }
        public string Runtime { get; set; }
        public double Popularity { get; set; }
        public string Revenue { get; set; }
        public string Description { get; set; }
    }
}