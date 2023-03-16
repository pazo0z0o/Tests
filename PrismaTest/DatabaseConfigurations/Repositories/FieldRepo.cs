//using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DatabaseConfigurations.Repositories
{
   

    public class FieldRepo : IEntityRepo<Fields>
    {//TODO: Field Repo crud

        private readonly IConfiguration _configuration;
        //injection
        public FieldRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Add(Fields entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public IList<Fields> GetAll()
        {
            throw new NotImplementedException();
        }

        public Fields? GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public void Update(int ID, Fields entity)
        {
            throw new NotImplementedException();
        }
    }
}
