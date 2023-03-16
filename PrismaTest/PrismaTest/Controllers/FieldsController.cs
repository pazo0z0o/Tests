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
    public class FieldsController : ControllerBase
    {
        private readonly FieldsRepo _fieldsRepo;

        public FieldsController(FieldsRepo fieldsRepo)
        {
            _fieldsRepo = fieldsRepo;
        }

        // I think that calling the fields through their selected formId is appropriate
        //GetAll seems unneccessary here

        // GET api/fields/id
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Fields>>> Get(int formid)
        {
            try
            {//it should take the Id of the form that those fields belong to
                var fields = await Task.Run(() => _fieldsRepo.GetByFormId(formid));
                if (fields == null)
                {
                    return NotFound();
                }
                return Ok(fields);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/fields
        [HttpPost]
        public async Task<IActionResult> PostAsync( Fields field, int formId)
        {
            try
            {
                await Task.Run(() => _fieldsRepo.Add(field, formId));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/fields/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Fields fields)
        {
            try
            {
                var existingFields = await Task.Run(() => _fieldsRepo.GetById(id));
                if (existingFields == null)
                {
                    return NotFound();
                }
                fields.Id = id;
                await Task.Run(() => _fieldsRepo.Update(fields));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/fields/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var existingFields = await Task.Run(() => _fieldsRepo.GetById(id));
                if (existingFields == null)
                {
                    return NotFound();
                }
                await Task.Run(() => _fieldsRepo.Delete(id));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
