
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Data.Dtos;
public class CreateMovieDTO {
    [Required(ErrorMessage = "O título do filme é obrigatório.")]
    [MinLength(2, ErrorMessage = "O título deve ter no minímo 2 caracteres.")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "O genêro do filme é obrigatório.")]
    [MinLength(2, ErrorMessage = "O genêro deve ter no minímo 2 caracteres.")]
    [StringLength(50, ErrorMessage = "O genêro deve ter no máximo 50 caracteres.")]
    public string Genre { get; set; }
    
    [Required(ErrorMessage = "A duração do filme é obrigatório.")]
    [Range(60, 460, ErrorMessage = "A duração do filme deve ser entre 60 à 460 minutos.")]
    public int Duration { get; set; }
}