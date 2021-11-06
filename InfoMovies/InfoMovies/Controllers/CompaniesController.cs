using AutoMapper;
using InfoMovies.Auth.Model;
using InfoMovies.Data.Dtos;
using InfoMovies.Data.Entities;
using InfoMovies.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoMovies.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompaniesRepository companiesRepository, IMapper mapper)
        {
            _companiesRepository = companiesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var companies = await _companiesRepository.GetAll();
            return companies.Select(o => _mapper.Map<CompanyDto>(o));
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<CompanyDto>> Get(int companyId)
        {
            var company = await _companiesRepository.Get(companyId);
            if (company == null) return NotFound($"Couldn't find a company with id of {companyId}");

            return Ok(_mapper.Map<CompanyDto>(company));
        }

        [HttpPost]
        [Authorize(Roles = InfoMoviesUserRoles.Admin)]
        public async Task<ActionResult<CompanyDto>> Create(CreateCompanyDto dto)
        {
            var company = _mapper.Map<Company>(dto);

            await _companiesRepository.Create(company);

            return Created($"/api/companies/{company.Id}", _mapper.Map<CompanyDto>(company));
        }

        [HttpPut("{companyId}")]
        [Authorize(Roles = InfoMoviesUserRoles.Admin)]
        public async Task<ActionResult<CompanyDto>> Edit(int companyId, CreateCompanyDto dto)
        {
            var company = await _companiesRepository.Get(companyId);
            if (company == null) return NotFound($"Couldn't find a company with id of {companyId}");

            _mapper.Map(dto, company);

            await _companiesRepository.Edit(company);

            return Ok(_mapper.Map<CompanyDto>(company));
        }

        [HttpDelete("{companyId}")]
        [Authorize(Roles = InfoMoviesUserRoles.Admin)]
        public async Task<ActionResult> Delete(int companyId)
        {
            var company = await _companiesRepository.Get(companyId);
            if (company == null) return NotFound($"Couldn't find a company with id of {companyId}");

            await _companiesRepository.Delete(company);

            // 204
            return NoContent();
        }
    }
}
