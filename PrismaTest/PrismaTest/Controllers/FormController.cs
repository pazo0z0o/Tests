using DatabaseConfigurations.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
namespace PrismaTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IEntityRepo<Forms> _formsRepo;
        //injection
        public FormController(IEntityRepo<Forms> form)
        {
            _formsRepo = form;

        }

        [HttpGet("/forms/formlist")]
        public async Task<ActionResult<IEnumerable<Forms>>> GetAllFormsAsync()
        {
            var forms = await Task.Run(() => _formsRepo.GetAll());
            return Ok(forms);
        }

        [HttpGet("/forms/{id:int}")]
        public async Task<ActionResult<Forms>> GetFormByIdAsync(int id)
        {
            var form = await Task.Run(() => _formsRepo.GetById(id));
            if (form == null)
            {
                return NotFound();
            }
            return Ok(form);
        }

        [HttpPost("/forms/create/")]
        public async Task<IActionResult> PostAsync([FromBody] Forms form)
        {
            if (form == null)
            {
                return BadRequest();
            }

            await Task.Run(() => _formsRepo.Add(form));
            return CreatedAtAction(nameof(GetFormByIdAsync), new { id = form.Id }, form);
        }

        [HttpPut("/forms/create/{id:int}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Forms form)
        {
            if (form == null || id != form.Id)
            {
                return BadRequest();
            }

            var existingForm = await Task.Run(() => _formsRepo.GetById(id));
            if (existingForm == null)
            {
                return NotFound();
            }

            await Task.Run(() => _formsRepo.Update(id, form));
            return NoContent();
        }

        [HttpDelete("/forms/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var existingForm = await Task.Run(() => _formsRepo.GetById(id));
            if (existingForm == null)
            {
                return NotFound();
            }

            await Task.Run(() => _formsRepo.Delete(id));
            return NoContent();
        }
    }
}
