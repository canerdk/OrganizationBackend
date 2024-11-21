using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repository;
using Entities.Entities;

namespace DataAccess.Concrete
{
    public class OrganizationDAL : EntityRepository<Organization>, IOrganizationDAL
    {
        public OrganizationDAL(AppDbContext context) : base(context)
        {
        }
    }
}
