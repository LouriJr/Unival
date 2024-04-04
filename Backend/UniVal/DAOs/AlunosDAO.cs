using MySql.Data.MySqlClient;
using UniVal.DTOs;

namespace UniVal.DAOs
{
    public class AlunosDAO
    {
        public void CadastrarAluno(AlunoDTO aluno)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Alunos (Nome, CPF, Email, Celular, DataNascimento) VALUES
						(@nome,@cpf,@email,@celular, @nascimento)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", aluno.Nome);
            comando.Parameters.AddWithValue("@cpf", aluno.CPF);
            comando.Parameters.AddWithValue("@email", aluno.Email);
            comando.Parameters.AddWithValue("@celular", aluno.Celular);
            comando.Parameters.AddWithValue("@nascimento", aluno.DataNascimento);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public List<AlunoDTO> ListarAlunos()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Alunos";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var alunos = new List<AlunoDTO>();

            while (dataReader.Read())
            {
                var aluno = new AlunoDTO();
                aluno.ID = int.Parse(dataReader["ID"].ToString());
                aluno.Nome = dataReader["Nome"].ToString();
                aluno.Email = dataReader["Email"].ToString();
                aluno.CPF = dataReader["CPF"].ToString();
                aluno.Celular = dataReader["Celular"].ToString();
                aluno.DataNascimento = DateTime.Parse(dataReader["DataNascimento"].ToString());

                alunos.Add(aluno);
            }
            conexao.Close();

            return alunos;
        }

        public AlunoDTO ListarAlunoPorID(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"SELECT Matriculas.ID as MatriculaID, Status, 
                          Materias.ID as MateriaID, Materias.Nome as MateriaNome, 
                          Professores.ID AS ProfessorID, Professores.Nome as Professor,
                          Aluno, Alunos.Nome as NomeAluno, Alunos.Email, Alunos.CPF, Alunos.Celular, Alunos.DataNascimento
                          FROM Matriculas 
                          INNER JOIN Alunos
                          ON Alunos.ID = Matriculas.Aluno
                          INNER JOIN Materias
                          ON Materias.ID = Matriculas.Materia
                          INNER JOIN Professores
                          ON Professores.ID = Materias.Professor
                          WHERE Aluno = @id;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);

            var dataReader = comando.ExecuteReader();

            var aluno = new AlunoDTO();
            while (dataReader.Read())
            {
                if (aluno.ID == 0)
                {
                    //Primeira vez que está executando a leitura
                    aluno.ID = int.Parse(dataReader["Aluno"].ToString());
                    aluno.Nome = dataReader["NomeAluno"].ToString();
                    aluno.Email = dataReader["Email"].ToString();
                    aluno.CPF = dataReader["CPF"].ToString();
                    aluno.Celular = dataReader["Celular"].ToString();
                    aluno.DataNascimento = DateTime.Parse(dataReader["DataNascimento"].ToString());
                    aluno.Matriculas = new List<MatriculaDTO>();
                }
                var materia = new MateriaDTO();
                materia.ID = int.Parse(dataReader["MateriaID"].ToString());
                materia.Nome = dataReader["MateriaNome"].ToString();

                var professor = new ProfessorDTO();
                professor.ID = int.Parse(dataReader["ProfessorID"].ToString());
                professor.Nome = dataReader["Professor"].ToString();

                materia.Professor = professor;

                var matricula = new MatriculaDTO();
                matricula.ID = int.Parse(dataReader["MatriculaID"].ToString());
                matricula.Status = dataReader["Status"].ToString();
                matricula.Materia = materia;

                aluno.Matriculas.Add(matricula);
            }
            conexao.Close();

            return aluno;
        }

        public List<AlunoDTO> BuscarPorNome(string nome)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Alunos WHERE Nome LIKE @nome";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", $"%{nome}%");

            var dataReader = comando.ExecuteReader();

            var alunos = new List<AlunoDTO>();

            while (dataReader.Read())
            {
                var aluno = new AlunoDTO();
                aluno.ID = int.Parse(dataReader["ID"].ToString());
                aluno.Nome = dataReader["Nome"].ToString();
                aluno.Email = dataReader["Email"].ToString();
                aluno.CPF = dataReader["CPF"].ToString();
                aluno.Celular = dataReader["Celular"].ToString();
                aluno.DataNascimento = DateTime.Parse(dataReader["DataNascimento"].ToString());

                alunos.Add(aluno);
            }
            conexao.Close();

            return alunos;
        }

        public void AtualizarMatricula(int matriculaId, string status)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"UPDATE Matriculas SET Status = @status WHERE ID = @id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", matriculaId);
            comando.Parameters.AddWithValue("@status", status);

            comando.ExecuteNonQuery();
            conexao.Close();

        }

        public void CadastrarMatricula(int idAluno, int idMateria)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Matriculas (Status, Aluno, Materia) VALUES ('Cursando', @id, @materia);";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", idAluno);
            comando.Parameters.AddWithValue("@materia", idMateria);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
