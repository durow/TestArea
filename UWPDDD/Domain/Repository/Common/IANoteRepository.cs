using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Common
{
    public interface IANoteRepository:IRepository<ANote>
    {
        ANote GetOne(int id);
        string GetContent(int id);
    }
}
