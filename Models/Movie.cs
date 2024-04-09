using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models;

public class Movie {

    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }

}