using System.Linq;

using Timesheets.Domain.DTO;

namespace Timesheets.Domain
{
    public interface IPersonRepository
    {
        IQueryable<IPerson> GetAll();
        IPerson GetById(int id);
        IPerson Create(PersonCreateDTO person);
        IPerson Update(PersonEditDTO person);
        IPerson Delete(int id);
    }
}
