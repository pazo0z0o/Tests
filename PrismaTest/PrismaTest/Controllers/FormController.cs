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
        public FormController( IEntityRepo<Forms> form)
        {
            _formsRepo = form;
           
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Forms>>> GetAllForms()
        {
            var forms = await Task.Run(() => _formsRepo.GetAll());
            return Ok(forms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Forms>> GetFormById(int id)
        {
            var form = await Task.Run(() => _formsRepo.GetById(id));
            if (form == null)
            {
                return NotFound();
            }
            return Ok(form);
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm([FromBody] Forms form)
        {
            if (form == null)
            {
                return BadRequest();
            }

            await Task.Run(() => _formsRepo.Add(form));
            return CreatedAtAction(nameof(GetFormById), new { id = form.Id }, form);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForm(int id, [FromBody] Forms form)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForm(int id)
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
