using MySql.Data.MySqlClient;
using UniVal.DTOs;

namespace UniVal.DAOs
{
    public class ProfessoresDAO
    {
        public void CadastrarProfessor(ProfessorDTO professor)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Professores (Nome, CPF, Email, Celular, DataNascimento) VALUES
						(@nome,@cpf,@email,@celular, @nascimento)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", professor.Nome);
            comando.Parameters.AddWithValue("@cpf", professor.CPF);
            comando.Parameters.AddWithValue("@email", professor.Email);
            comando.Parameters.AddWithValue("@celular", professor.Celular);
            comando.Parameters.AddWithValue("@nascimento", professor.DataNascimento);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public void RemoverProfessor(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"DELETE FROM Professores WHERE ID = @id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public List<MateriaDTO> ListarMateriasProfessor(int idProfessor)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Materias WHERE Professor = @id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", idProfessor);

            var dataReader = comando.ExecuteReader();

            var materias = new List<MateriaDTO>();

            while (dataReader.Read())
            {
                var materia = new MateriaDTO();
                materia.ID = int.Parse(dataReader["ID"].ToString());
                materia.Nome = dataReader["Nome"].ToString();
                materia.Descricao = dataReader["Descricao"].ToString();

                materias.Add(materia);
            }
            conexao.Close();

            return materias;
        }

        internal List<ProfessorDTO> ListarProfessores()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Professores";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var professores = new List<ProfessorDTO>();

            while (dataReader.Read())
            {
                var professor = new ProfessorDTO();
                professor.ID = int.Parse(dataReader["ID"].ToString());
                professor.Nome = dataReader["Nome"].ToString();
                professor.Email = dataReader["Email"].ToString();
                professor.CPF = dataReader["CPF"].ToString();
                professor.Celular = dataReader["Celular"].ToString();
                professor.DataNascimento = DateTime.Parse(dataReader["DataNascimento"].ToString());

                professores.Add(professor);
            }
            conexao.Close();

            return professores;
        }
    }
}
