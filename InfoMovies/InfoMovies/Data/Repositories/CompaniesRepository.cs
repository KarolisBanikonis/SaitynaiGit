using InfoMovies.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoMovies.Data.Repositories
{
    public interface ICompaniesRepository
    {
        Task Delete(Company company);
        Task<List<Company>> GetAll();
        Task<Company> Get(int companyId);
        Task<Company> Create(Company company);
        Task Edit(Company company);
    }

    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly InfoMoviesContext _infoMoviesContext;
        public CompaniesRepository(InfoMoviesContext infoMoviesContext)
        {
            _infoMoviesContext = infoMoviesContext;
        }

        public async Task<Company> Get(int companyId)
        {
            return await _infoMoviesContext.Companies.FirstOrDefaultAsync(o => o.Id == companyId);
        }

        public async Task<List<Company>> GetAll()
        {
            return await _infoMoviesContext.Companies.ToListAsync();
        }

        public async Task<Company> Create(Company company)
        {
            _infoMoviesContext.Companies.Add(company);
            await _infoMoviesContext.SaveChangesAsync();
            return company;
        }

        public async Task Edit(Company company)
        {
            _infoMoviesContext.Companies.Update(company);
            await _infoMoviesContext.SaveChangesAsync();
        }

        public async Task Delete(Company company)
        {
            _infoMoviesContext.Companies.Remove(company);
            await _infoMoviesContext.SaveChangesAsync();
        }
    }
}
