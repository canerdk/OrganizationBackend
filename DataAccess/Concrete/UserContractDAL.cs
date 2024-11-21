using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repository;
using Entities.Entities;

namespace DataAccess.Concrete
{
    public class UserContractDAL : EntityRepository<UserContract>, IUserContractDAL
    {
        public UserContractDAL(AppDbContext context) : base(context)
        {
        }
    }
}
