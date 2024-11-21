using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repository;
using Entities.Entities;

namespace DataAccess.Concrete
{
    public class ParticipantDAL : EntityRepository<Participant>, IParticipantDAL
    {
        public ParticipantDAL(AppDbContext context) : base(context)
        {
        }
    }
}
