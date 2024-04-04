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
    public class ProfessoresController : ControllerBase
    {
        [HttpGet]
        public IActionResult Listar()
        {
            var dao = new ProfessoresDAO();
            var professores = dao.ListarProfessores();

            return Ok(professores);
        }
        [HttpPost]
        public IActionResult Cadastrar([FromBody] ProfessorDTO professor)
        {
            var dao = new ProfessoresDAO();
            dao.CadastrarProfessor(professor);

            return Ok();
        }

        [HttpDelete]
        public IActionResult RemoverProfessor(int id)
        {
            var dao = new ProfessoresDAO();
            var materiasProfessor = dao.ListarMateriasProfessor(id);

            if (materiasProfessor.Count > 0)
            {
                return BadRequest("Não foi possivel remover o professor");
            }

            dao.RemoverProfessor(id);

            return Ok();
        }
    }
}
