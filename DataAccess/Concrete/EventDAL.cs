using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repository;
using Entities.Entities;

namespace DataAccess.Concrete
{
    public class EventDAL : EntityRepository<Event>, IEventDAL
    {
        public EventDAL(AppDbContext context) : base(context)
        {
        }
    }
}
