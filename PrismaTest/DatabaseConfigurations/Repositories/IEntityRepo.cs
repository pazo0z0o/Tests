using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConfigurations.Repositories
{
    public interface IEntityRepo<TEntity>
    {//We use Generics (TEntity ) since we need for the interface to apply to any type.
        IList<TEntity> GetAll(); //self-explanatory, returns a list of all the Forms
        TEntity? GetById(int ID); //Returns a single Form entity
        void Add(TEntity entity); //Adds an Forms entity 
        void Update(int ID, TEntity entity);    //Edits entity
        void Delete(int ID);                   // Deletes entity
    }
}
