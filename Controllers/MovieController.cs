namespace MoviesApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data;
using MoviesApi.Data.Dtos;
using MoviesApi.Models;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{

    private readonly MovieContext _context;
    private readonly IMapper _mapper;

    public MovieController(
        MovieContext context,
        IMapper mapper
    )
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpPost]
    public IActionResult AddMovie([FromBody] CreateMovieDTO movieDTO)
    {
        Movie movie = _mapper.Map<Movie>(movieDTO);

        _context.Movies.Add(movie);
        _context.SaveChanges();
        return Created("Movie", movie);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReadMovieDTO>> GetMovies([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {        
        var movies = _context.Movies.Skip(skip).Take(take);

        var moviesViewDTO = _mapper.Map<List<ReadMovieDTO>>(movies);

        return Ok(moviesViewDTO);
    }

    [HttpGet("{id}")]
    public ActionResult<ReadMovieDTO> GetById(int id)
    {
        var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

        if (movie == null)
        {
            return NotFound(new { message = "Filme não encontrado." });
        }

        var movieViewDTO = _mapper.Map<ReadMovieDTO>(movie);

        return Ok(movieViewDTO);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDTO movieDTO) 
    {

        var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

        if (movie == null) 
        {
            return NotFound(new { message = "Filme não encontrado." });
        }

        _mapper.Map(movieDTO, movie);

        _context.Movies.Update(movie);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult UpdatePacthMovie(int id, JsonPatchDocument<UpdateMovieDTO> moviePATCH) 
    {

        var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null) 
        {
            return NotFound(new { message = "Filme não encontrado." });
        }

        var movieToUpdate = _mapper.Map<UpdateMovieDTO>(movie);

        moviePATCH.ApplyTo(movieToUpdate, ModelState);

        if (!TryValidateModel(movieToUpdate)) 
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(movieToUpdate, movie);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id) 
    {

        var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null) 
        {
            return NotFound(new { message = "Filme não encontrado." });
        }

        _context.Movies.Remove(movie);
        _context.SaveChanges();

        return NoContent();
    }
}
