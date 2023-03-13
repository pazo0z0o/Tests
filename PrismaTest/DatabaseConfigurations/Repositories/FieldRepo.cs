using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConfigurations.Repositories
{
    public class FieldRepo : IEntityRepo<Fields>
    {//TODO: Field Repo crud
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
