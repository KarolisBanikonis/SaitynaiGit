using AutoMapper;
using InfoMovies.Data.Dtos;
using InfoMovies.Data.Dtos.Auth;
using InfoMovies.Data.Entities;

namespace InfoMovies.Data
{
    public class InfoMoviesProfile : Profile
    {
        public InfoMoviesProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CreateCompanyDto, Company>();
            CreateMap<Movie, MovieDto>();
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CreateCommentDto, Comment>();
            //Auth
            CreateMap<InfoMoviesUser, UserDto>();
        }
    }
}
