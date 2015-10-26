using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Common
{
    public interface IRepository<TModel>
    {
        IEnumerable<TModel> GetAll();
        void Delete(TModel model);
        void Insert(TModel model);
        void Update(TModel model);
    }
}
