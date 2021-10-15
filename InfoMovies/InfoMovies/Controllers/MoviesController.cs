using AutoMapper;
using InfoMovies.Data.Dtos;
using InfoMovies.Data.Entities;
using InfoMovies.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoMovies.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/movies")]
    public class MoviesController : ControllerBase
    {
        
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMapper _mapper;
        private readonly ICompaniesRepository _companiesRepository;

        public MoviesController(IMoviesRepository moviesRepository, IMapper mapper, ICompaniesRepository companiesRepository)
        {
            _moviesRepository = moviesRepository;
            _mapper = mapper;
            _companiesRepository = companiesRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieDto>> GetAll(int companyId)
        {
            var movies = await _moviesRepository.GetAll(companyId);
            return movies.Select(o => _mapper.Map<MovieDto>(o));
        }

        // /api/companies/1/movies/2
        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieDto>> Get(int companyId, int movieId)
        {
            var movie = await _moviesRepository.Get(companyId, movieId);
            if (movie == null) return NotFound($"Couldn't find a movie with id of {movieId}");

            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> Create(int companyId, CreateMovieDto dto)
        {
            var company = await _companiesRepository.Get(companyId);
            if (company == null) return NotFound($"Couldn't find a company with id of {companyId}");

            var movie = _mapper.Map<Movie>(dto);
            movie.CompanyId = companyId;

            await _moviesRepository.Create(movie);

            return Created($"/api/companies/{companyId}/movies/{movie.Id}", _mapper.Map<MovieDto>(movie));
        }

        [HttpPut("{movieId}")]
        public async Task<ActionResult<MovieDto>> Edit(int companyId, int movieId, CreateMovieDto dto)
        {
            var company = await _companiesRepository.Get(companyId);
            if (company == null) return NotFound($"Couldn't find a company with id of {companyId}");

            var oldMovie = await _moviesRepository.Get(companyId, movieId);
            if (oldMovie == null)
                return NotFound($"Couldn't find a movie with id of {movieId}");

            _mapper.Map(dto, oldMovie);

            await _moviesRepository.Edit(oldMovie);

            return Ok(_mapper.Map<MovieDto>(oldMovie));
        }

        [HttpDelete("{movieId}")]
        public async Task<ActionResult> Delete(int companyId, int movieId)
        {
            var movie = await _moviesRepository.Get(companyId, movieId);
            if (movie == null)
                return NotFound($"Couldn't find a movie with id of {movieId}");

            await _moviesRepository.Delete(movie);

            // 204
            return NoContent();
        }
    }
}
