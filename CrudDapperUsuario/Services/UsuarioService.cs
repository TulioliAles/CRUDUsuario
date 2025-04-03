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

        public async Task<ResponseModel<Usuario>> BuscaUsuarioById(int usuarioId)
        {
            ResponseModel<Usuario> response = new ResponseModel<Usuario>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.QueryFirstOrDefaultAsync<Usuario>("select * from Usuarios where id = @Id", new {Id = usuarioId});

                if(usuarioBanco == null)
                {
                    response.Mensagem = "Nenhum usuário localizado.";
                    response.Status = false;
                    return response;
                }

                response.Dados = usuarioBanco;
                response.Mensagem = "Usuário localizado com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.ExecuteAsync("insert into Usuarios(NomeCompleto, Email, Cargo, Salario, CPF, Senha, Situacao) " +
                    "values(@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)", usuarioCriarDto);

                if (usuariosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar o registro!";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);
                var usuarioMapper = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuarioMapper;
                response.Mensagem = "Usuários listados com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.ExecuteAsync("update Usuarios set NomeCompleto = @NomeCompleto," +
                    "                                                                  Email = @Email, Cargo = @Cargo, Salario = @Salario," +
                    "                                                                  CPF = @CPF, Situacao = @Situacao" +
                    "                                                                  where Id = @Id", usuarioEditarDto);

                if (usuariosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a edição!";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);
                var usuarioMapper = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuarioMapper;
                response.Mensagem = "Usuários listados com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> RemoverUsuario(int usuarioId)
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.ExecuteAsync("delete from Usuarios where id = @Id", new { Id = usuarioId });

                if (usuariosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao remover o registro!";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);
                var usuarioMapper = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuarioMapper;
                response.Mensagem = "Usuários listados com sucesso!";
            }

            return response;
        }
       
        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
        {
            return await connection.QueryAsync<Usuario>("select * from Usuarios");
        }
    }
}
