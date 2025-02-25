using CrudDapperUsuario.DTO;
using CrudDapperUsuario.Models;

namespace CrudDapperUsuario.Services
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios();
    }
}
