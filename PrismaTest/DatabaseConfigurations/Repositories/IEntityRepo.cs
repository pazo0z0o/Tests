using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConfigurations.Repositories
{
    public interface IEntityRepo<TEntity> 
    {
        IList<TEntity> GetAll(); //View Part of Crud
        TEntity? GetById(int ID); //
        void Add(TEntity entity);
        void Update(int ID, TEntity entity);
        void Delete(int ID);
    }
}
