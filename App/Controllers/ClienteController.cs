using App.Data;
using App.DTOs;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers {
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase {

        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public ActionResult<Cliente> Get(
            [FromQuery] string nomeFiltro
            ) {
            var query = _context.Clientes.AsQueryable();

            if(string.IsNullOrEmpty(nomeFiltro)) {
                query = query.Where(c => c.Nome == nomeFiltro);
            }

            return Ok(query.ToList());
        }
        

        [HttpPost]
        public ActionResult<Cliente> Post(
            [FromBody] ClienteDTO cliente
            ) {
            Cliente clienteNovo = new(){
                Nome = cliente.Nome,
                DataNascimento = cliente.DataNascimento
            };

            _context.Clientes.Add(clienteNovo);
            _context.SaveChanges();
            return Created("/api/clientes",clienteNovo);
        }

        [HttpPut("{id}")]
        public ActionResult<Cliente> Put([FromBody] ClienteDTO body,
                                         [FromRoute] int id) {
            var cliente = _context.Clientes.Find(id);
            if(cliente != null) {
                cliente.Nome = body.Nome;
                cliente.DataNascimento = body.DataNascimento;
            }
            _context.SaveChanges();
            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id) {
            var cliente = _context.Clientes.Find(id);
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return Ok();
        }
    }
}
