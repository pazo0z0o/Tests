using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConfigurations.Repositories
{
    public  interface IEntityField<TEntity>
    {   //We use Generics (TEntity ) since we need for the interface to apply to any type.
        IList<TEntity> GetAll(); //self-explanatory, returns a list of all the Fields/Not implemented
        IList<TEntity> GetByFormId(int ID); //Returns a List of TEntity based on their FormId
        TEntity? GetById(int ID); //The same but with Fields.Id value
        void Add(TEntity entity, int formid); //Adds an Fields entity that has a specific FormId
        void Update(TEntity entity); //Edits entity
        void Delete(int ID);        // Deletes entity
    }
}
