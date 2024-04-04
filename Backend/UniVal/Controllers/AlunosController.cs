using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniVal.DAOs;
using UniVal.DTOs;

namespace UniVal.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        [HttpGet]
        [Route("listar")]
        public IActionResult ListarAlunos()
        {
            var dao = new AlunosDAO();
            var alunos = dao.ListarAlunos();

            return Ok(alunos);
        }

        [HttpGet]
        [Route("listarPorID")]
        public IActionResult ListarAlunoPorID(int id)
        {
            var dao = new AlunosDAO();
            var aluno = dao.ListarAlunoPorID(id);

            return Ok(aluno);
        }

        [HttpGet]
        [Route("buscarPorNome")]
        public IActionResult BuscarPorNome(string nome)
        {
            var dao = new AlunosDAO();
            var alunos = dao.BuscarPorNome(nome);

            return Ok(alunos);
        }

        [HttpPost]
        [Route("CadastrarAluno")]
        public IActionResult CadastrarAluno([FromBody] AlunoDTO aluno)
        {
            var dao = new AlunosDAO();
            dao.CadastrarAluno(aluno);

            return Ok();
        }

        [HttpPost]
        [Route("Matricular")]
        public IActionResult MatricularAluno(int idAluno, int idMateria)
        {
            var alunoDAO = new AlunosDAO();
            var aluno = alunoDAO.ListarAlunoPorID(idAluno);

            var materiaDAO = new MateriasDAO();
            var materia = materiaDAO.ListarMateriaPorId(idMateria);

            var alunoJaConcluiuMateria = VerificarSeAlunoJaConcluiuMateria(aluno, materia);
            if (alunoJaConcluiuMateria)
            {
                return BadRequest("Aluno já concluiu a matéria");
            }

            var alunoEstaCursandoMateria = VerificarSeAlunoEstaCursandoMateria(aluno, materia);
            if (alunoEstaCursandoMateria)
            {
                return BadRequest("Aluno está cursando a matéria");
            }

            var idMatriculaTrancada = VerificarSeAlunoTemMateriaTrancada(aluno, materia);
            if (idMatriculaTrancada != 0)
            {
                alunoDAO.AtualizarMatricula(idMatriculaTrancada, "Cursando");
                return Ok();
            }

            var alunoNaoConcluiuPendencias = VerificarSeAlunoNaoConcluiuDependenciasDaMateria(aluno, materia);
            if (alunoNaoConcluiuPendencias)
            {
                return BadRequest("Aluno não concluiu as pendencias");
            }

            alunoDAO.CadastrarMatricula(idAluno, materia.ID);
            return Ok();
        }

        private bool VerificarSeAlunoJaConcluiuMateria(AlunoDTO aluno, MateriaDTO materia)
        {
            if (aluno.Matriculas is null) return false;
            foreach (var matricula in aluno.Matriculas)
            {
                if (matricula.Materia.ID == materia.ID && matricula.Status == "Concluido")
                {
                    return true;
                }
            }

            return false;
        }

        private bool VerificarSeAlunoEstaCursandoMateria(AlunoDTO aluno, MateriaDTO materia)
        {
            if (aluno.Matriculas is null) return false;
            foreach (var matricula in aluno.Matriculas)
            {
                if (matricula.Materia.ID == materia.ID && matricula.Status == "Cursando")
                {
                    return true;
                }
            }

            return false;
        }
        private int VerificarSeAlunoTemMateriaTrancada(AlunoDTO aluno, MateriaDTO materia)
        {
            if (aluno.Matriculas is null) return 0;

            foreach (var matricula in aluno.Matriculas)
            {
                if (matricula.Materia.ID == materia.ID && matricula.Status == "Trancada")
                {
                    return matricula.ID;
                }
            }

            return 0;
        }
        private bool VerificarSeAlunoNaoConcluiuDependenciasDaMateria(AlunoDTO aluno, MateriaDTO materia)
        {
            if (materia.Dependencias is null) return true;

            var dependenciasConcluidas = 0;

            foreach (var dependencia in materia.Dependencias)
            {

                foreach (var matricula in aluno.Matriculas)
                {
                    if (matricula.Materia.ID == dependencia.ID && matricula.Status == "Concluido")
                    {
                        dependenciasConcluidas++;
                    }
                }
            }

            return materia.Dependencias.Count != dependenciasConcluidas;
        }

        [HttpPut]
        [Route("AtualizarMatricula")]
        public IActionResult AtualizarMatricula(int matriculaId, string status)
        {
            var dao = new AlunosDAO();
            dao.AtualizarMatricula(matriculaId, status);

            return Ok();
        }

    }
}
