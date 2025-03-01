using AutoMapper;
using CrudDapperUsuario.DTO;
using CrudDapperUsuario.Models;

namespace CrudDapperUsuario.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Usuario, UsuarioListarDto>();
        }
    }
}
