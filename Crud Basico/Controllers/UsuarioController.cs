using Crud_Basico.Data;
using Crud_Basico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_Basico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("ListarUsuarios")]
        public async Task<ActionResult<List<Usuario>>> ListarUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("ObterUsuario/{id}")]
        public async Task<ActionResult<Usuario>> ObterUsuario(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            return usuario;
        }

        [HttpPost("CriarUsuario")]
        public async Task<ActionResult> CriarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterUsuario), new { id = usuario.Id }, usuario);

        }

        [HttpPut("AtualizarUsuario/{id}")]
        public async Task<ActionResult<List<Usuario>>> AtualizarUsuario(int id, Usuario usuarioAtualizado)
        {
            // buscando o usuário no banco de dados
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            // verificando se o usuário existe
            if(usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }
            //atualizando os dados do usuário
            usuario.Name = usuarioAtualizado.Name;
            usuario.Idade = usuarioAtualizado.Idade;

            // salvando as alterações
            _context.Usuarios.Update(usuario);  
            await _context.SaveChangesAsync();

            return await _context.Usuarios.ToListAsync();

        }

        [HttpDelete("DeletarUsuario/{id}")]
        public async Task<ActionResult<List<Usuario>>> DeletarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if(usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return await _context.Usuarios.ToListAsync();
        }
    }
}
