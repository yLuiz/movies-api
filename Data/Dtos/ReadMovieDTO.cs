namespace MoviesApi.Data.Dtos;

public class ReadMovieDTO 
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }
    public DateTime ConsultTime { get; set; } = DateTime.Now;
}