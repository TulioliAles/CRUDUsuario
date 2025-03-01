using AutoMapper;
using CrudDapperUsuario.DTO;
using CrudDapperUsuario.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CrudDapperUsuario.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios()
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>(); 

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.QueryAsync<Usuario>("select * from Usuarios");

                if(usuarioBanco.Count() == 0)
                {
                    response.Mensagem = "Nenhum usuário localizado!";
                    response.Status = false;
                    return response;
                }

                var usuarioMapper = _mapper.Map<List<UsuarioListarDto>>(usuarioBanco);

                response.Dados = usuarioMapper;
                response.Mensagem = "Usuários localizados com sucesso!";
            }

            return response;
        }
    }
}
