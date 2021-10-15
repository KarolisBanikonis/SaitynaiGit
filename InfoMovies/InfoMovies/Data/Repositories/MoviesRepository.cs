using InfoMovies.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoMovies.Data.Repositories
{
    public interface IMoviesRepository
    {
        Task Delete(Movie movie);
        Task<List<Movie>> GetAll(int companyId);
        Task<Movie> Get(int companyId, int movieId);
        Task<Movie> Create(Movie movie);
        Task Edit(Movie movie);
    }

    public class MoviesRepository : IMoviesRepository
    {
        private readonly InfoMoviesContext _infoMoviesContext;

        public MoviesRepository(InfoMoviesContext infoMoviesContext)
        {
            _infoMoviesContext = infoMoviesContext;
        }

        public async Task<Movie> Get(int companyId, int movieId)
        {
            return await _infoMoviesContext.Movies.FirstOrDefaultAsync(o => o.CompanyId == companyId && o.Id == movieId);
        }

        public async Task<List<Movie>> GetAll(int companyId)
        {
            return await _infoMoviesContext.Movies.Where(o => o.CompanyId == companyId).ToListAsync();
        }

        public async Task<Movie> Create(Movie movie)
        {
            _infoMoviesContext.Movies.Add(movie);
            await _infoMoviesContext.SaveChangesAsync();
            return movie;
        }

        public async Task Edit(Movie movie)
        {
            _infoMoviesContext.Movies.Update(movie);
            await _infoMoviesContext.SaveChangesAsync();
        }

        public async Task Delete(Movie movie)
        {
            _infoMoviesContext.Movies.Remove(movie);
            await _infoMoviesContext.SaveChangesAsync();
        }
    }
}
