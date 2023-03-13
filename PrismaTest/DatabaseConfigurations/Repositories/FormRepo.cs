using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConfigurations.Repositories
{
    public class FormRepo : IEntityRepo<Forms>
    {//TODO: Form Repo crud
        public void Add(Forms entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public IList<Forms> GetAll()
        {
            throw new NotImplementedException();
        }

        public Forms? GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public void Update(int ID, Forms entity)
        {
            throw new NotImplementedException();
        }
    }
}
