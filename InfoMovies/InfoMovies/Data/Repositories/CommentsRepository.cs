using InfoMovies.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoMovies.Data.Repositories
{
    public interface ICommentsRepository
    {
        Task Delete(Comment comment);
        Task<List<Comment>> GetAll(int companyId, int movieId);
        Task<Comment> Get(int companyId, int movieId, int commentId);
        Task<Comment> Create(Comment comment);
        Task Edit(Comment comment);
    }

    public class CommentsRepository : ICommentsRepository
    {
        private readonly InfoMoviesContext _infoMoviesContext;

        public CommentsRepository(InfoMoviesContext infoMoviesContext)
        {
            _infoMoviesContext = infoMoviesContext;
        }

        public async Task<Comment> Get(int companyId, int movieId, int commentId)
        {
            return await _infoMoviesContext.Comments.FirstOrDefaultAsync(o => o.Movie.CompanyId == companyId && o.MovieId == movieId && o.Id == commentId);
        }

        public async Task<List<Comment>> GetAll(int companyId, int movieId)
        {
            return await _infoMoviesContext.Comments.Where(o => o.Movie.CompanyId == companyId && o.MovieId == movieId).ToListAsync();
        }

        public async Task<Comment> Create(Comment comment)
        {
            _infoMoviesContext.Comments.Add(comment);
            await _infoMoviesContext.SaveChangesAsync();
            return comment;
        }

        public async Task Edit(Comment comment)
        {
            _infoMoviesContext.Comments.Update(comment);
            await _infoMoviesContext.SaveChangesAsync();
        }

        public async Task Delete(Comment comment)
        {
            _infoMoviesContext.Comments.Remove(comment);
            await _infoMoviesContext.SaveChangesAsync();
        }
    }
}
