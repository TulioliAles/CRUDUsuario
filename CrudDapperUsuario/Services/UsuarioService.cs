using CrudDapperUsuario.DTO;
using CrudDapperUsuario.Models;

namespace CrudDapperUsuario.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;

        public UsuarioService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios()
        {
            throw new NotImplementedException();
        }
    }
}
