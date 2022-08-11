using App.Data;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers {
    [ApiController]
    [Route("api/vacinas")]
    public class VacinaController : ControllerBase {

        private readonly AppDbContext _context;

        public VacinaController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Vacina>> Get(
            [FromQuery] string? nomeFiltro,
            [FromQuery] int? numeroDoses
            ) {

            var query = _context.Vacina.AsQueryable();
            if(!string.IsNullOrEmpty(nomeFiltro)) {
                query = query.Where(v => v.Nome == nomeFiltro);
            }
            if(numeroDoses.HasValue && numeroDoses > 0) {
                query = query.Where(v => v.NumeroDoses == numeroDoses);
            }

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Vacina> GetById(
            [FromRoute] int id
            ) {
            return Ok(_context.Vacina.Find(id));
        }

        [HttpPost]
        public ActionResult<Vacina> Post(
            [FromBody] Vacina vacina
            ) {
            _context.Vacina.Add(vacina);

            var operacoesRealizadas = _context.SaveChanges();

            if(operacoesRealizadas == 0)
                return new StatusCodeResult(500);
            return Created("/api/vacinas",vacina);
        }

        [HttpPut("{id}")]
        public ActionResult<Vacina> Put(
            [FromBody] Vacina vacina,
            [FromRoute] int id
            ) {
            var vacinaAlterada = _context.Vacina.Find(id);

            if(vacinaAlterada == null) return NotFound();

            vacinaAlterada.Nome = vacina.Nome;
            vacinaAlterada.NumeroDoses = vacina.NumeroDoses;

            _context.SaveChanges();

            return Ok(vacinaAlterada);

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id) {
            var vacinaExcluida = _context.Vacina.Find(id);

            if(vacinaExcluida == null) return NotFound();

            _context.Vacina.Remove(vacinaExcluida);

            var operacoesRealizadas = _context.SaveChanges();

            if(operacoesRealizadas == 0)
                return new StatusCodeResult(500);

            return NoContent();
        }
    }
}