using MySql.Data.MySqlClient;
using UniVal.DTOs;

namespace UniVal.DAOs
{
    public class UsuariosDAO
    {
        public void Cadastrar(UsuarioDTO usuario)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Usuarios (Email, Senha) VALUES
						(@email,@senha)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public bool VerificarUsuario(UsuarioDTO usuario)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Usuarios WHERE email = @email";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@email", usuario.Email);

            var dataReader = comando.ExecuteReader();

            var usuarios = new List<UsuarioDTO>();

            while (dataReader.Read())
            {
                usuario = new UsuarioDTO();
                usuario.ID = int.Parse(dataReader["ID"].ToString());
                usuario.Email = dataReader["Email"].ToString();
                usuario.Senha = dataReader["Senha"].ToString();

                usuarios.Add(usuario);
            }
            conexao.Close();

            return usuarios.Count > 0;
        }

        public UsuarioDTO Login(UsuarioDTO usuario)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Usuarios WHERE email = @email and senha = @senha";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);

            var dataReader = comando.ExecuteReader();

            usuario = new UsuarioDTO();

            while (dataReader.Read())
            {
                usuario.ID = int.Parse(dataReader["ID"].ToString());
                usuario.Email = dataReader["Email"].ToString();
                usuario.Senha = dataReader["Senha"].ToString();
            }
            conexao.Close();

            return usuario;
        }
    }
}
