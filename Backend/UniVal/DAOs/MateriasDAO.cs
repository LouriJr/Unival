using MySql.Data.MySqlClient;
using System;
using UniVal.DTOs;

namespace UniVal.DAOs
{
    public class MateriasDAO
    {
        public void CadastrarMateria(MateriaDTO materia)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Materias (Nome, Descricao, Professor) VALUES
						(@nome,@desc, @prof);
                        SELECT LAST_INSERT_ID();";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", materia.Nome);
            comando.Parameters.AddWithValue("@desc", materia.Descricao);
            comando.Parameters.AddWithValue("@prof", materia.Professor.ID);

            materia.ID = Convert.ToInt32(comando.ExecuteScalar());
            conexao.Close();

            CadastrarDependencias(materia);
        }

        private void CadastrarDependencias(MateriaDTO materia)
        {
            if(materia.Dependencias is null)
            {
                return;
            }
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO DependenciasMateria (Materia, Dependencia) VALUES
						(@materia, @dependencia)";


            foreach (var dependencia in materia.Dependencias)
            {
                var comando = new MySqlCommand(query, conexao);
                comando.Parameters.AddWithValue("@materia", materia.ID);
                comando.Parameters.AddWithValue("@dependencia", dependencia.ID);

                comando.ExecuteNonQuery();
            }

            conexao.Close();
        }

        public List<MateriaDTO> ListarMaterias()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Materias";

            var comando = new MySqlCommand(query, conexao);
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

        public MateriaDTO ListarMateriaPorId(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Materias Where ID =@id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);

            var dataReader = comando.ExecuteReader();

            var materia = new MateriaDTO();

            while (dataReader.Read())
            {
                materia.ID = int.Parse(dataReader["ID"].ToString());
                materia.Nome = dataReader["Nome"].ToString();
                materia.Descricao = dataReader["Descricao"].ToString();
            }
            conexao.Close();

            materia.Matriculas = ListarMatriculasMateria(materia.ID);
            materia.Dependencias = ListarDependenciasMateria(materia.ID);

            return materia;
        }

        private List<MatriculaDTO> ListarMatriculasMateria(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"SELECT Matriculas.ID as MatriculaID, Status, Aluno, Nome, Email, CPF, Celular, DataNascimento
                          FROM Matriculas 
                          INNER JOIN Alunos
                          ON Alunos.ID = Matriculas.Aluno
                          WHERE Materia = @id;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);

            var dataReader = comando.ExecuteReader();

            var matriculas = new List<MatriculaDTO>();

            while (dataReader.Read())
            {
                var matricula = new MatriculaDTO();
                matricula.ID = int.Parse(dataReader["MatriculaID"].ToString());
                matricula.Status = dataReader["Status"].ToString();

                var aluno = new AlunoDTO();
                aluno.ID = int.Parse(dataReader["Aluno"].ToString());
                aluno.Nome = dataReader["Nome"].ToString();
                aluno.Email = dataReader["Nome"].ToString();
                aluno.CPF = dataReader["Nome"].ToString();
                aluno.Celular = dataReader["Nome"].ToString();
                aluno.DataNascimento = DateTime.Parse(dataReader["DataNascimento"].ToString());

                matricula.Aluno = aluno;

                matriculas.Add(matricula);
            }
            conexao.Close();

            return matriculas;
        }

        private List<MateriaDTO> ListarDependenciasMateria(int idMateria)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"SELECT*FROM DependenciasMateria 
                            INNER JOIN Materias 
                            ON Materias.ID = DependenciasMateria.Dependencia
                            Where DependenciasMateria.Materia = @idMateria;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@idMateria", idMateria);

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
    }
}
