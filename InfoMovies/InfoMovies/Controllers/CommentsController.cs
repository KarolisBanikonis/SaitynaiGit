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
    [Route("api/companies/{companyId}/movies/{movieId}/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMapper _mapper;
        private readonly IMoviesRepository _moviesRepository;
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IAuthorizationService _authorizationService;

        public CommentsController(ICommentsRepository commentsRepository, IMapper mapper, IMoviesRepository moviesRepository, ICompaniesRepository companiesRepository, IAuthorizationService authorizationService)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
            _moviesRepository = moviesRepository;
            _companiesRepository = companiesRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentDto>> GetAll(int companyId, int movieId)
        {
            var comments = await _commentsRepository.GetAll(companyId, movieId);
            return comments.Select(o => _mapper.Map<CommentDto>(o));
        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentDto>> Get(int companyId, int movieId, int commentId)
        {
            var comment = await _commentsRepository.Get(companyId, movieId, commentId);
            if (comment == null) return NotFound($"Couldn't find a comment with id of {commentId}");

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        [HttpPost]
        [Authorize(Roles = InfoMoviesUserRoles.SimpleUser)]
        public async Task<ActionResult<CommentDto>> Create(int companyId, int movieId, CreateCommentDto dto)
        {
            var company = await _companiesRepository.Get(companyId);
            if (company == null) return NotFound($"Couldn't find a company with id of {companyId}");

            var movie = await _moviesRepository.Get(companyId, movieId);
            if (movie == null) return NotFound($"Couldn't find a movie with id of {movieId}");

            var comment = _mapper.Map<Comment>(dto);
            comment.MovieId = movieId;
            //Put user id
            comment.UserId = User.FindFirst(CustomClaims.UserId).Value;

            await _commentsRepository.Create(comment);

            return Created($"/api/companies/{companyId}/movies/{movieId}/comments/{comment.Id}", _mapper.Map<CommentDto>(comment));
        }

        [HttpPut("{commentId}")]
        [Authorize(Roles = InfoMoviesUserRoles.SimpleUser)]
        public async Task<ActionResult<CommentDto>> Edit(int companyId, int movieId, int commentId, CreateCommentDto dto)
        {
            var company = await _companiesRepository.Get(companyId);
            if (company == null) return NotFound($"Couldn't find a company with id of {companyId}");

            var movie = await _moviesRepository.Get(companyId, movieId);
            if (movie == null) return NotFound($"Couldn't find a movie with id of {movieId}");

            var oldComment = await _commentsRepository.Get(companyId, movieId, commentId);
            if (oldComment == null) return NotFound($"Couldn't find a comment with id of {commentId}");

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, oldComment, PolicyNames.SameUser);

            if (!authorizationResult.Succeeded)
                //403
                return Forbid();

            _mapper.Map(dto, oldComment);

            await _commentsRepository.Edit(oldComment);

            return Ok(_mapper.Map<CommentDto>(oldComment));
        }

        [HttpDelete("{commentId}")]
        [Authorize(Roles = InfoMoviesUserRoles.SimpleUser)]
        public async Task<ActionResult> Delete(int companyId, int movieId, int commentId)
        {
            var comment = await _commentsRepository.Get(companyId, movieId, commentId);
            if (comment == null) return NotFound($"Couldn't find a comment with id of {commentId}");

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, comment, PolicyNames.SameUser);

            if (!authorizationResult.Succeeded)
                //403
                return Forbid();

            await _commentsRepository.Delete(comment);

            return NoContent();
        }
    }
}
