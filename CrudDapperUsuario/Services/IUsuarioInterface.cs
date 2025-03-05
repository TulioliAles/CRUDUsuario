using CrudDapperUsuario.DTO;
using CrudDapperUsuario.Models;

namespace CrudDapperUsuario.Services
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios();
        Task<ResponseModel<UsuarioListarDto>> BuscaUsuarioById(int usuarioId);
        Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario (UsuarioCriarDto usuarioCriarDto);
        Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto);
        Task<ResponseModel<List<UsuarioListarDto>>> RemoverUsuario(int usuarioId);
    }
}
