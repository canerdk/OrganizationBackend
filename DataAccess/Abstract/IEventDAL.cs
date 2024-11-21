using DataAccess.Repository;
using Entities.Entities;

namespace DataAccess.Abstract
{
    public interface IEventDAL : IEntityRepository<Event>
    {
    }
}
