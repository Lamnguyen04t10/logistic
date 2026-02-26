using System.Application.Commands.CommandModels;
using System.Domain.Entities.UserAgr;
using AutoMapper;

namespace System.Application.AutoMappers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, LoginCommandResponseModel>();
        }
    }
}
