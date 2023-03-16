using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConfigurations.Repositories
{
    public  interface IEntityField<TEntity>
    {
        IList<TEntity> GetAll();
        IList<TEntity> GetByFormId(int ID);
        TEntity? GetById(int ID);
        void Add(TEntity entity, int formid);
        void Update(TEntity entity);
        void Delete(int ID);
    }
}
