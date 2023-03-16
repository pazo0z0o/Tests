using DatabaseConfigurations.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
namespace PrismaTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IEntityRepo<Forms> _formsRepo;
        //injection
        public FormController(IConfiguration configuration, IEntityRepo<Forms> form)
        {
            _formsRepo = form;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Forms>> GetAll()
        {
            var result = _formsRepo.GetAll();

            return result.Select(form => new Forms
            {
                Id = form.Id,
                Title = form.Title,
                Description = form.Description,
                DateOfCreation = form.DateOfCreation,
                LastUpdated = form.LastUpdated,

                Fields = form.Fields.Select(field => new Fields
                {
                    Id = field.Id,
                    NoteName = field.NoteName,
                    Note = field.Note,
                }).ToList()
            });
        }

        [HttpGet("{id:int}")]
        public async Task<Forms> GetById(int id) 
        {
            var result = _formsRepo.GetById(id);





            return result;
        }
    }
}
