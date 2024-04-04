using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniVal.DAOs;
using UniVal.DTOs;

namespace UniVal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MateriasController : ControllerBase
    {
        [HttpPost]
        public IActionResult CadastrarMateria([FromBody] MateriaDTO materia)
        {
            var dao = new MateriasDAO();
            dao.CadastrarMateria(materia);

            return Ok();
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult ListarMaterias()
        {
            var dao = new MateriasDAO();
            var materias = dao.ListarMaterias();

            return Ok(materias);
        }

        [HttpGet]
        [Route("listarPorID")]
        public IActionResult ListarMateriaPorID(int id)
        {
            var dao = new MateriasDAO();
            var materias = dao.ListarMateriaPorId(id);

            return Ok(materias);
        }
    }
}
